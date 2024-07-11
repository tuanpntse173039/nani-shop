using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParams
    {
        private const int MAX_PAGE_SIZE = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 2;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : _pageSize;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        private string _search = string.Empty;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}