using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductSpecifications
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;

        private const int MaxPageSize = 50;

        private int _PageSize = 6;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? int.MaxValue : value;
        }

        private string? _Search;

        public string? Search
        {
            get => _Search;
            set => _Search = value?.Trim().ToLower();
        }

    }
}
