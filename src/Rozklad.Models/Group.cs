using System;

namespace Rozklad.Models
{
    public record Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}