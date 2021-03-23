/// <summary>
/// Класс для сериализации входящего сообщения при запросе GetUpdates
/// </summary>

public class GetUpdates
{
    public bool ok { get; set; }
    public ResultUp[] result { get; set; }
}

public class ResultUp
{
    public int update_id { get; set; }
    public Message message { get; set; }
}

public class Message
{
    public int message_id { get; set; }
    public From from { get; set; }
    public Chat chat { get; set; }
    public int date { get; set; }
    public string text { get; set; }
    public Document document { get; set; }
}

public class From
{
    public int id { get; set; }
    public bool is_bot { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string language_code { get; set; }
}

public class Chat
{
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string type { get; set; }
}

public class Document
{
    public string file_name { get; set; }
    public string mime_type { get; set; }
    public Thumb thumb { get; set; }
    public string file_id { get; set; }
    public string file_unique_id { get; set; }
    public int file_size { get; set; }
}

public class Thumb
{
    public string file_id { get; set; }
    public string file_unique_id { get; set; }
    public int file_size { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}
