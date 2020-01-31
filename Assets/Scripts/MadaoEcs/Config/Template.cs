
namespace MadaoEcs {
    public abstract class Template {

        public abstract Template Clone();
        protected abstract void FillTemplateClone(Template template);
    }
}
