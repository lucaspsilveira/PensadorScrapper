using PensadorScrapper.Models;
using PensadorScrapper.Repository;
using PensadorScrapper.Services;
using System;
using System.Collections.Generic;

namespace PensadorScrapper
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ScrapperService scrapper = new ScrapperService();
            
            List<string> autores = new List<string>{
                "seneca",
                "clarice_lispector" ,
                "friedrich_nietzsche",
                "confucio",
                "aristoteles",
                "platao",
                "socrates",
                "voltaire",
                "arthur_schopenhauer",
                "william_shakespeare",
                "francis_bacon",
                "bertrand_russell",
                "jean_paul_sartre",
                "albert_camus",
                "simone_de_beauvoir",
                "thomas_hobbes",
                "santo_agostinho",
                "jean_jacques_rousseau",
                "rene_descartes",
                "immanuel_kant",
                "john_locke",
                "blaise_pascal",
                "galileu_galilei",
                "michel_de_montaigne",
                "mary_wollstonecraft",
                "angela_davis",
                "hipatia_de_alexandria",
                "maquiavel",
                "adam_smith",
                "zygmunt_bauman",
                "baruch_espinosa",
                "friedrich_engels",
                "georg_wilhelm_friedrich_hegel",
                "soren_kierkegaard",
                "epicteto",
                "martin_heidegger",
                "michel_foucault",
                "martin_heidegger",
                "hannah_arendt"
            };

            foreach (var autor in autores)
            {
                string assunto = "autor/"+autor;

                Console.WriteLine($"\n\n\n\n\n\nAUTOR: {autor} \n\n\n");
                
                var totalPaginas = await scrapper.GetTotalPages(assunto);
                Console.WriteLine("N de paginas: " + totalPaginas);

                for (var i = 1; i <= totalPaginas; i++)
                {
                    await foreach (var quote in scrapper.GetQuotes(assunto, i))
                    {
                        Console.WriteLine($"ID: {quote.Id} | Autor: {quote.Autor.Trim()} | Frase: {quote.Texto.Trim()}");
                        QuotesRepository.SaveQuote(quote);
                    }
                    Console.WriteLine($"\n --------Página {i}--------- \n\n");
                }
            }
            
            
        }
    }
}
