using Newtonsoft.Json;
using ClosedXML.Excel;

try
{

    string cep = string.Empty;

    while (true)
    {
        Console.WriteLine("Digite o cep: ");

        cep = Console.ReadLine();
        cep = cep.Replace(".", "".Replace("-", ""));

        if (int.TryParse(cep, out _) && cep.Length == 8)
        {
            break;
        }
        else
        {
            Console.WriteLine("CEP inválido.");
        }
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

        ViaCepModel viaCepModel = JsonConvert.DeserializeObject<ViaCepModel>(responseBody);
        Console.WriteLine("" + responseBody);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("CEP");
        worksheet.Cells("A1").Value = "CEP";
        worksheet.Cells("B1").Value = "Logradouro";
        worksheet.Cells("C1").Value = "Complemento";
        worksheet.Cells("D1").Value = "Bairro";
        worksheet.Cells("E1").Value = "Localidade";
        worksheet.Cells("F1").Value = "UF";

        worksheet.Cells("A2").Value = viaCepModel.Cep;
        worksheet.Cells("B2").Value = viaCepModel.Logradouro;
        worksheet.Cells("C2").Value = viaCepModel.Complemento;
        worksheet.Cells("D2").Value = viaCepModel.Bairro;
        worksheet.Cells("E2").Value = viaCepModel.Localidade;
        worksheet.Cells("F2").Value = viaCepModel.UF;

        workbook.SaveAs($"Planilha_{DateTime.Now:ddMMyyyyHHmmss}.xlsx");

    }
}


catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro: {ex.Message}");
}
