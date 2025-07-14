using Xunit;
using task13;
using System.Text.Json;

public class SerializerTest
{
    Student student = new Student
    {
        FirstName = "Daniil",
        LastName = "Mazunin",
        BirthDate = new DateTime(2006, 1, 1),
        Grades = new List<Subject>
        {
            new Subject { Name = "Programming", Grade = 4},
            new Subject { Name = "Math", Grade = 5}
        }
    };
    Student nullStudent = new Student
    {
        FirstName = "NullStudent",
        LastName = null,
        BirthDate = new DateTime(2006, 5, 5),
        Grades = null
    };
    [Fact]
    public void Serialize_ReturnsCorrectJsonStructure()
    {
        string json = Serializer.Serialize(student);

        Assert.Contains("\"FirstName\": \"Daniil\"", json);
        Assert.Contains("\"LastName\": \"Mazunin\"", json);
        Assert.Contains("\"BirthDate\": \"01.01.2006\"", json);
        Assert.Contains("\"Name\": \"Programming\"", json);
        Assert.Contains("\"Grade\": 4", json);
    }

    [Fact]
    public void Serialize_DoesNotIncludeGradesAndLastNameInJson()
    {
        string json = Serializer.Serialize(nullStudent);

        Assert.DoesNotContain("\"LastName\": null", json);
        Assert.DoesNotContain("\"Grades\": null", json);
        Assert.DoesNotContain("\"Grades\": []", json);
    }

    [Fact]
    public void Deserialize_ReturnStudent()
    {
        string json = @"{
            ""FirstName"": ""Daniil"",
            ""LastName"": ""Mazunin"",
            ""BirthDate"": ""01.01.2006"",
            ""Grades"": [
            { ""Name"": ""Programming"", ""Grade"": 4 },
            { ""Name"": ""Math"", ""Grade"": 5 }
            ]
        }";

        Student student = Serializer.Deserialize(json);

        Assert.Equal("Daniil", student.FirstName);
        Assert.Equal("Mazunin", student.LastName);
        Assert.Equal(new DateTime(2006, 1, 1), student.BirthDate);
        Assert.Equal(2, student.Grades!.Count);
        Assert.Equal("Programming", student.Grades[0].Name);
        Assert.Equal(4, student.Grades[0].Grade);
    }

    [Fact]
    public void Deserialize_ThrowsException()
    {
        string invalidJson = @"{
            ""FirstName"": ""NullStudent"",
            ""LastName"": """",
            ""BirthDate"": ""05.05.2006""
        }";

        Assert.Throws<JsonException>(() => Serializer.Deserialize(invalidJson));
    }
    [Fact]
    public void SaveToFileAndLoadFromFile_ReturnsEquivalentStudent()
    {
        string path = Path.Combine(Path.GetTempPath(), "student.json");

        Serializer.SaveToFile(student, path);
        Student loadedStudent = Serializer.LoadFromFile(path);

        Assert.Equal(student.FirstName, loadedStudent.FirstName);
        Assert.Equal(student.LastName, loadedStudent.LastName);
        Assert.Equal(student.BirthDate, loadedStudent.BirthDate);
        Assert.Equal(student.Grades!.Count, loadedStudent.Grades!.Count);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
