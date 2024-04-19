using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

try
{

    string cep = string.Empty;

    while (true)
    {
        Console.WriteLine("Digite o cep: ");
        cep = cep.Replace(".", "".Replace("-", ""));

        if (int.TryParse(cep, out _) && cep.Length < 8)
        {
            break;
        }
        else
        {
            Console.WriteLine("CEP inválido.");
        }

        string viaCepUrl = $"https://viacep.com.br/ws/{cep}/json/";

        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(viaCepUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody.Contains("\"erro\": true"))
            {
                Console.WriteLine("Cep não localizado");
            }

            // ViaCepModel viaCepModel = JsonConvert.
        }
    }


}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro: {ex.Message}");
}
