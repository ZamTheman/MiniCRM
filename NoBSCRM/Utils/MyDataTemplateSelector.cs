using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NoBSCRM.Utils
{
    public class MyDataTemplateSelector : DataTemplateSelector
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
