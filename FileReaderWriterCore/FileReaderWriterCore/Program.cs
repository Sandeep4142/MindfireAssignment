Console.WriteLine("Welcome to File Reader/Writer App!");
string path = "D:\\Mindfire\\FileReaderWriterCore\\FileReaderWriterCore\\";

while (true)
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1. Write to file");
    Console.WriteLine("2. Read from file");
    Console.WriteLine("3. Exit");

    string option = Console.ReadLine();

    switch (option)
    {
        case "1":
            WriteToFile(path);
            break;
        case "2":
            ReadFromFile(path);
            break;
        case "3":
            Console.WriteLine("Exiting program...");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
    Console.WriteLine("\n ---------------- \n");
}

static void WriteToFile(string path)
{
    Console.WriteLine("Enter the file name:");
    string fileName = Console.ReadLine();

    Console.WriteLine("Enter the content to write to the file:");
    string content = Console.ReadLine();

    string filePath = Path.Combine(path, fileName);

    try
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(content);
        }

        Console.WriteLine($"Content successfully written to {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
    }
}

static void ReadFromFile(string path)
{
    Console.WriteLine("Enter the file name to read from:");
    string fileName = Console.ReadLine();

    string filePath = Path.Combine(path, fileName);

    try
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File '{fileName}' does not exist.");
            return;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine("Content read from file:");
            Console.WriteLine(content);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while reading from the file: {ex.Message}");
    }
}

