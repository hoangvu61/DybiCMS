
namespace Dybi.App.Features
{
    public class TreeNode
    {
        public object Id { get; set; }
        public string Text { get; set; }
        public string Style { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}
