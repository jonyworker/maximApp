using maximApp.WebHost.Components;
using maximApp.SharedUI.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// ✅ 靜態檔案（wwwroot、_content/...）由這個處理
app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddAdditionalAssemblies(typeof(maximApp.SharedUI.Components.MauiRoot).Assembly)
    .AddInteractiveServerRenderMode();

app.Run();
