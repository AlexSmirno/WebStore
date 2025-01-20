namespace WebStore.AuthServer.Services
{
    public class JWTProvider
    {
        public string GenerateJWT()
        {
            return "JUWATY";
        }

        public bool VerifyJWT(string jwt)
        {
            return jwt == "JUWATY";
        }
    }
}
