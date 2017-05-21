namespace UsfQuiz.Web
{
    using System.Web.Optimization;
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {            
            RegisterJs(bundles);
            RegisterCss(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterJs(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/libs/jquery/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/libs/bootstrap/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/libs/angularjs/angular.min.js",
                "~/Scripts/libs/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/libs/angularjs-toaster/toaster.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/solve-quiz").Include(
                "~/Scripts/app/controllers/solve-quiz.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/new-quiz").Include(
                "~/Scripts/app/controllers/newQuiz.js",
                "~/Scripts/libs/bootstrap-switch/toggle-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/categories").Include(
                "~/Scripts/libs/ng-crop/ng-img-crop.js",
                "~/Scripts/libs/ng-file-upload/ng-file-upload-all.js",
                "~/Scripts/app/controllers/manageCategories.js"));
        }

        private static void RegisterCss(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/styles/bootstrap/bootstrap.min.css",
                "~/Content/styles/font-awesome/font-awesome.min.css",
                "~/Content/styles/style.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-switch").Include(
                "~/Content/styles/bootstrap-switch/switch.css"));
            bundles.Add(new StyleBundle("~/Content/toaster").Include(
                "~/Content/styles/toaster.css"));

            bundles.Add(new StyleBundle("~/Content/solve").Include(
                "~/Content/styles/quiz.css"));
        }
    }
}
