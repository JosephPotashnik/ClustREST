using DensityPeaksClustering;

var builder = WebApplication.CreateBuilder(args);

// Register CORS services before building the app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddHttpClient();

// Configure Kestrel to listen on a non-privileged port (e.g., port 80 or 5106)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);  // Listen on HTTP port 80 (or another port like 5106)
});

var app = builder.Build();

// Optional: Force HTTP to HTTPS redirection (useful if your ALB doesn't handle this)
app.UseHttpsRedirection();

// Apply CORS middleware
app.UseCors("AllowAll");

// Set up endpoints
app.MapGet("/", () =>
{
    return "Hello Clustering Service!";
});

app.MapPost("/KNN", (KNNAlgorithmParams p) =>
{
    var result = DensityPeaksClusteringAlgorithms.KNN(p);
    return Results.Json(result);
});

app.MapPost("/DensityPeaks", (DensityPeaksAlgorithmParams p) =>
{
    var result = DensityPeaksClusteringAlgorithms.DPClustering(p);
    return Results.Json(result);
});

app.MapPost("/MultiManifold", (MultiManifoldAlgorithmParams p) =>
{
    var result = DensityPeaksClusteringAlgorithms.MultiManifold(p);
    return Results.Json(result);
});

app.Run();