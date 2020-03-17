using CodeSqlGenerate.Utility;

namespace CodeSqlGenerate.Generate
{
    public static class Backend_Code
    {
        public static readonly string BackendOutputPath = Program.OutputPath + "JavaCode\\";

        public static readonly string EntityPath = "entity\\";
        public static readonly string DaoPath = "dao\\";
        public static readonly string MapperPath = "mappers\\";
        public static readonly string ControllerPath = "controller\\";
        public static readonly string ServiceImplPath = "serviceimpl\\";
        public static readonly string ServiceInterfacePath = "service\\";

        public static readonly string BaseEntityPackagePrefix = "com.infinite.icts.entity";
        public static readonly string BaseDaoPackagePrefix = "com.infinite.icts.dao";
        public static readonly string BaseControllerPackagePrefix = "com.infinite.icts.controller";
        public static readonly string BaseServiceImplPackagePrefix = "com.infinite.icts.serviceimpl";
        public static readonly string BaseServiceInterfacePackagePrefix = "com.infinite.icts.service";

        public static void PreProcessFolder(string folderPrefix,
            out string entityFolderPath,
            out string daoFolderPath,
            out string mapperFolderPath,
            out string controllerFolderPath,
            out string serviceImplFolderPath,
            out string serviceInterfaceFolderPath)
        {
            entityFolderPath = BackendOutputPath + EntityPath + folderPrefix;
            daoFolderPath = BackendOutputPath + DaoPath + folderPrefix;
            mapperFolderPath = BackendOutputPath + MapperPath + folderPrefix;
            controllerFolderPath = BackendOutputPath + ControllerPath + folderPrefix;
            serviceImplFolderPath = BackendOutputPath + ServiceImplPath + folderPrefix;
            serviceInterfaceFolderPath = BackendOutputPath + ServiceInterfacePath + folderPrefix;

            if (Program.IsOutputToProject)
            {
                entityFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + EntityPath + folderPrefix;
                daoFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + DaoPath + folderPrefix;
                mapperFolderPath = @"C:\0_Workspace\icts\src\main\resources\" + MapperPath + folderPrefix;
                controllerFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + ControllerPath + folderPrefix;
                serviceImplFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + ServiceImplPath + folderPrefix;
                serviceInterfaceFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + ServiceInterfacePath + folderPrefix;
            }

            CommonMethod.CreateDirectoryIfNotExist(entityFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(daoFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(mapperFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(controllerFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(serviceImplFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(serviceInterfaceFolderPath);

            if (!string.IsNullOrEmpty(folderPrefix))
            {
                CommonMethod.ClearFolderIfExistFiles(entityFolderPath);
                CommonMethod.ClearFolderIfExistFiles(daoFolderPath);
                CommonMethod.ClearFolderIfExistFiles(mapperFolderPath);
                CommonMethod.ClearFolderIfExistFiles(controllerFolderPath);
                CommonMethod.ClearFolderIfExistFiles(serviceImplFolderPath);
                CommonMethod.ClearFolderIfExistFiles(serviceInterfaceFolderPath);
            }
        }

        public static string GetPagingParameterEntity()
        {
            return "com.infinite.common.entity.PagingParameter";
        }
    }
}
