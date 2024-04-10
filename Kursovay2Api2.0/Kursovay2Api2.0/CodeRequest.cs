
public class CodeRequest
{
    Dictionary<string, string> codeMail = new Dictionary<string, string>();

    internal string? GetCode(string? mail)
    {
        if (codeMail.ContainsKey(mail))
            return codeMail[mail];
        return "";
    }

    internal void SetCode(string? mail, string code)
    {
        if (codeMail.ContainsKey(mail))
            codeMail[mail] = code;
        else
            codeMail.Add(mail, code);
    }
}