using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;

public class MeuMiddlewareCustom{
    // Request Delegate é o responsavel por chamar o proximo middleware no pipeline
    private readonly RequestDelegate _next;

    //Construtor para o middleware receber uma injeção de dependencia do requestDelegate
    public MeuMiddlewareCustom(RequestDelegate next){
        _next = next;
    }

    // ACAO DO MEU MIDDLEWARE - ESTE METODO É CHAMADO TODA VEZ QUE MEUMIDDLEWARE É CHAMADO
    public async Task InvokeAsync(HttpContext context){
        Console.WriteLine("\n\r ----------- ANTES ---------- \n\r");

        // chamo o proximo middleware dentro do pipeline passando o contexto da requisição. Await para aguardar o retorno dos proximos middlewares.
        await _next(context);

        Console.WriteLine("\n\r ----------- DEPOIS ---------- \n\r");
    }
}

// ESTENSAO DO  IApplicationBuilder PARA SIMPLIFICAR A CHAMADA.
public static class MeuMiddlewareCustomExtension
{
    public static IApplicationBuilder UseMeuMiddleware(this IApplicationBuilder builder){
        return builder.UseMiddleware<MeuMiddlewareCustom>();
    }
}