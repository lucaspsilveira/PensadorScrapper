using System;
using System.Collections.Generic;
using System.Text;

namespace PensadorScrapper.Models
{
    class Quote
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Texto { get; set; }

        public Quote(int id, string autor, string texto)
        {
            Id = id;
            Autor = autor;
            Texto = texto;
        }
    }
}
