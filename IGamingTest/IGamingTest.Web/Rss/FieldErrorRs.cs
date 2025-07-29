namespace IGamingTest.Web.Rss;

public record FieldErrorRs(
    int? Code,
    string Message,
    string Field
    ) : MessageErrorRs(
        Code,
        Message);

