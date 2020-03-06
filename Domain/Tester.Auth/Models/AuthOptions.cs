namespace Tester.Auth
{
    public class AuthOptions
    {
        public string Secret { get; set; }
        public int Duration { get; } = 7;
    }
}