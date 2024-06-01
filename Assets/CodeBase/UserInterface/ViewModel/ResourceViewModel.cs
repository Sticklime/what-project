using Unity.Properties;

namespace CodeBase.UserInterface.ViewModel
{
    public class ResourceViewModel
    {
        [CreateProperty] public string CountResource { get; set; }

        public ResourceViewModel(int countResource)
        {
            CountResource = countResource.ToString();
        }
    }
}