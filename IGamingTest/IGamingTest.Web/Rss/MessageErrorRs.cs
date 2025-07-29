namespace IGamingTest.Web.Rss;

public record MessageErrorRs(
    int? Code,
    string Message
    ) : BaseErrorRs(
        Code);
