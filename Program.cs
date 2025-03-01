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
var app = builder.Build();

// Apply CORS middleware
app.UseCors("AllowAll");

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