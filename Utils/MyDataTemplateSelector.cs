using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Utils
{
    public class MyDataTemplateSelector : Windows.UI.Xaml.Controls.DataTemplateSelector
    {
        public TemplateCollection Templates { get; set; }
        private IList<Template> _templateCache { get; set; }

        public MyDataTemplateSelector()
        {
            
        }

        private void InitTemplateCollection()
        {
            _templateCache = Templates.ToList();
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if(_templateCache == null)
                InitTemplateCollection();

            if (item != null)
            {
                var dataType = item.GetType().ToString();
                string[] tempArray = dataType.Split('.');
                var match = _templateCache.Where(m => m.DataType == tempArray[tempArray.Length-1]).FirstOrDefault();

                if (match != null)
                    return match.DataTemplate;
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
