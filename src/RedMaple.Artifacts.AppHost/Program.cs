var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("db");

var apiService = builder.AddProject<Projects.RedMaple_Artifacts_ApiService>("redmaple-artifacts-api")
    .WithReference(postgresdb);

builder.AddProject<Projects.RedMaple_Artifacts_Web>("redmaple-artifacts-app")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
