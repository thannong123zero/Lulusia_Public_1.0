using Common;
using Common.ViewModels.VOCViewModelModels;
using Newtonsoft.Json;
using VOCBusinessLogic.IHelpers;

namespace VOCBusinessLogic.Helpers
{
    public class VOCUISettingHelper : IVOCUISettingHelper
    {
        private VOCUIConfigurationViewModel _contentVOCUI;
        private const string _path = @".\LocalData\UIVOCPageContent.json";
        public VOCUISettingHelper()
        {
            _contentVOCUI = new VOCUIConfigurationViewModel();
            using (StreamReader reader = new StreamReader(_path))
            {
                string json = reader.ReadToEnd();
                _contentVOCUI = JsonConvert.DeserializeObject<VOCUIConfigurationViewModel>(json);
            }
        }
        public VOCUIConfigurationViewModel GetAll()
        {
            return _contentVOCUI;
        }
        public IEnumerable<ContentUIViewModel> GetAll(int vocTypeId)
        {
            return vocTypeId == (int)EVOCType.Mall ? _contentVOCUI.Mall : _contentVOCUI.Office;
        }

        public ContentUIViewModel GetByKey(int vocTypeId, string key)
        {
            var obj = vocTypeId == (int)EVOCType.Mall ? _contentVOCUI.Mall : _contentVOCUI.Office;
            var contentItem = obj.FirstOrDefault(s => string.Equals(s.Key, key.ToString()));
            return contentItem;
        }

        public void Update(ContentUIViewModel model, int vocTypeId)
        {
            var contentList = vocTypeId == (int)EVOCType.Mall ? _contentVOCUI.Mall : _contentVOCUI.Office;
            var contentItem = contentList.FirstOrDefault(s => string.Equals(s.Key, model.Key));
            if (contentItem == null)
            {
                return;
            }
            contentItem.EN = model.EN;
            contentItem.VN = model.VN;
            string json = JsonConvert.SerializeObject(_contentVOCUI);
            File.WriteAllText(_path, json);
        }


    }
}
