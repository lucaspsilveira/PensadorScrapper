using AngleSharp;
using AngleSharp.Dom;
using PensadorScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PensadorScrapper.Services
{
    class ScrapperService
    {
        private IBrowsingContext context { get; set; }

        public ScrapperService()
        {
            var config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
        }


        public async Task<int> GetTotalPages(string assunto)
        {
            
            var document = await context.OpenAsync($"https://www.pensador.com/{assunto}");

            var quotesHtml = document.QuerySelectorAll("#content > div.phrases-list > .thought-card");
            var textoTotal = "";
            
            int quantidadePorPagina;
            int total;
            
            if (document.QuerySelector("#content > div.top > div.total") != null)
            {
                textoTotal = document.QuerySelector("#content > div.top > div.total").Text();
                if (int.TryParse(Regex.Match(textoTotal, @"(?<=de )(.*)(?= pensamentos )").Value, out total))
                {
                    quantidadePorPagina = int.Parse(Regex.Match(textoTotal, @"(?<=-\n)(.\d)(?=)").Value);
                    total = int.Parse(Regex.Match(textoTotal, @"(?<=de )(.*)(?= pensamentos )").Value);
                }
                else
                {
                    total = int.Parse(Regex.Replace(textoTotal, @"[^\d]", ""));
                    quantidadePorPagina = quotesHtml.Count();
                }
            }
            else
            {
                total = int.Parse(document.QuerySelector("#content > div.autorTotal > strong:nth-child(2)").Text());
                var textoPagina = document.QuerySelector("#content > div.autorTotal > strong:nth-child(1)").Text();
                quantidadePorPagina = int.Parse(Regex.Match(textoPagina, @"(?<= )(.\d)(?=)").Value);
            }
            
            int paginas = total / quantidadePorPagina;

            Console.WriteLine("Total: " + total);
            Console.WriteLine("Paginas: " + paginas);

            return paginas;
        }

        public async IAsyncEnumerable<Quote> GetQuotes(string assunto, int pagina)
        {
            var url = $"https://www.pensador.com/{assunto}/{pagina}";
            Console.WriteLine(url);
            var document = await context.OpenAsync(url);

            var quotesHtml = document.QuerySelectorAll("#content > div.phrases-list > .thought-card");
            
            foreach (var item in quotesHtml)
            {
                var autor = item.QuerySelector(".autor").Text();

                int id = int.Parse(item.GetAttribute("data-id"));

                var texto = item.QuerySelector(".frase").Text();

                if(item.QuerySelector(".insert-onlist") != null)
                {
                    continue;
                }

                yield return new Quote(id, autor.Trim(), texto.Trim());
            }
        }
    }
}
