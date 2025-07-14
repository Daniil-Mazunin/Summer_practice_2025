using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class Subject
{
	public string Name { get; set; }
	public int Grade { get; set; }
	public Subject() { }
	public Subject(string name, int grade)
	{
		Name = name;
		Grade = grade;
	}
}
public class Student
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime BirthDate { get; set; }
	public List<Subject> Grades { get; set; }
	public Student() { }
	public Student(string firstName, string lastName, DateTime birthDate, List<Subject> grades)
	{
		FirstName = firstName;
		LastName = lastName;
		BirthDate = birthDate;
		Grades = grades;
	}
}
public class DateTimeConverter : JsonConverter<DateTime>
{
	private string Format = "dd.MM.yyyy";
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return DateTime.ParseExact(reader.GetString()!, Format, null);
	}
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString(Format));
	}
}
public class Serializer
{
	public static string Serialize(Student student)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			Converters = { new DateTimeConverter() }
		};
		return JsonSerializer.Serialize(student, options);
	}
	public static Student Deserialize(string json)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			Converters = { new DateTimeConverter() }
		};
		var student = JsonSerializer.Deserialize<Student>(json, options);
		if (student == null)
		{
			throw new JsonException("Ошибка десериализации");
		}
		if (string.IsNullOrWhiteSpace(student.FirstName))
		{
			throw new JsonException("FirstName не указано");
		}
		if (string.IsNullOrWhiteSpace(student.LastName))
		{
			throw new JsonException("LastName не указано");
		}
		if (student.BirthDate > DateTime.Now)
		{
			throw new JsonException("Дата указана некоректно");
		}
		if (student.Grades != null)
		{
			foreach (var subject in student.Grades)
			{
				if (subject.Grade < 1 || subject.Grade > 5)
				{
					throw new JsonException("Оценка должна быть в диапозоне от 1 до 5");
				}
			}
		}
		return student;
	}
	public static void SaveToFile(Student student, string path)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new Exception("Путь к файлу отстуствует");
		}
		string json = Serialize(student);
		File.WriteAllText(path, json);
	}
	public static Student LoadFromFile(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new Exception("Путь к файлу отстуствует");
		}
		if (!File.Exists(path))
		{
			throw new Exception("Файл не найден");
		}
		string json = File.ReadAllText(path);
		return Deserialize(json);
	}
}
