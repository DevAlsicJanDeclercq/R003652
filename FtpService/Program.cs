using FluentFTP;

namespace FtpService;

internal static class Program {
	private static void Main(string[] args) {
		string ftphost = "ftp.meetnetvlaamsebanken.be";
		string publicIpUri = "https://api.ipify.org/";

		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		WebApplication app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.MapGet("/FileList", async (context) => {
			try {
				using FtpClient client = new(ftphost) {
					Credentials = new System.Net.NetworkCredential("R003652", "pw-R003652")
				};

				client.Connect();
				await TypedResults.Ok(client.GetListing().Select(x => new FtpFile( x.FullName,new DateTimeOffset(x.Modified, TimeSpan.Zero)))).ExecuteAsync(context);
			} catch (Exception ex) {
				await TypedResults.Problem(ex.ToString()).ExecuteAsync(context);
			}

		})
		.WithName("FileList")
		.WithOpenApi();

		app.MapGet("/PublicIP", async (x) => {
			try {
				using HttpClient c = new();
				string result = await (await c.GetAsync(publicIpUri)).Content.ReadAsStringAsync();

				await TypedResults.Ok(result).ExecuteAsync(x);
			} catch (Exception ex) {
				await TypedResults.Problem(ex.ToString()).ExecuteAsync(x);
			}
		});

		app.Run();
	}
}

record FtpFile( string Name,DateTimeOffset Modified);

