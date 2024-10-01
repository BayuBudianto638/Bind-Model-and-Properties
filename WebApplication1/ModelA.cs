using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    [ModelBinder]
    public class ModelA
    {
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Value { get; set; }
    }
}
