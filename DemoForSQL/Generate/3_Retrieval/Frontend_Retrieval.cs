using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._3_Retrieval
{
    public static class Frontend_Retrieval
    {
        public static string Get_RetrievalAll_Url = $"RETRIEVA_ALLL_DEVICE";
        public static string Get_RetrievalDetail_Url = $"RETRIEVAL_DETAIL_DEVICE";

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Generate_RetrievalDetail_TsContent(tableList);

        }

        private static void Generate_RetrievalDetail_TsContent(List<HotchnerTable> tableList)
        {
            var detailFolder = Frontend_Code.FrontendOutputPath;
            if (Program.IsOutputToProject)
            {
                detailFolder = @"C:\0_Workspace\ict\src\app\ict\content-modules\statistical-analysis\equip-table\";
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("import {Component, Input, OnInit} from '@angular/core';");
            stringBuilder.AppendLine("import {API} from '../../../../common/api';");
            stringBuilder.AppendLine("import {HttpService} from '../../../../common/services/http.service';");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Component({");
            stringBuilder.AppendLine("  selector: 'app-equip-table',");
            stringBuilder.AppendLine("  templateUrl: './equip-table.component.html',");
            stringBuilder.AppendLine("  styleUrls: ['./equip-table.component.scss']");
            stringBuilder.AppendLine("})");
            stringBuilder.AppendLine("export class EquipTableComponent implements OnInit {");
            stringBuilder.AppendLine("  @Input() columns: any[];");
            stringBuilder.AppendLine("  @Input() value: any[];");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  headerDictionary: any;");
            stringBuilder.AppendLine("  columnDictionary: any;");
            stringBuilder.AppendLine("  displayHeader: string;");
            stringBuilder.AppendLine("  displayDetail: boolean;");
            stringBuilder.AppendLine("  detailColumns: any[];");
            stringBuilder.AppendLine("  detailData: any;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  constructor(private httpService: HttpService) {");
            // title 字典
            stringBuilder.AppendLine("    this.headerDictionary = {");
            foreach (var table in tableList)
            {
                stringBuilder.AppendLine($"      {table.PascalMethodName}: '{table.Label}',");
            }
            stringBuilder.AppendLine("    };");
            // 字段名 字典
            stringBuilder.AppendLine("    this.columnDictionary = {");
            foreach (var table in tableList)
            {
                stringBuilder.AppendLine($"      {table.PascalMethodName}: [");
                for (int i = 0; i < table.RowList.Count; i++)
                {
                    var row = table.RowList[i];
                    if (i > Program.DetailViewColumnCount)
                    {
                        stringBuilder.AppendLine("        // {field: '" + row.Name.ToLower() + "', header: '" + row.Description + "'},");
                    }
                    else
                    {
                        stringBuilder.AppendLine("        {field: '" + row.Name.ToLower() + "', header: '" + row.Description + "'},");
                    }
                }
                stringBuilder.AppendLine("      ],");
            }
            stringBuilder.AppendLine("    };");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  ngOnInit() {");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  viewDetail(rowData) {");
            stringBuilder.AppendLine("    this.displayHeader = this.headerDictionary[rowData.deviceType];");
            stringBuilder.AppendLine("    this.detailColumns = this.columnDictionary[rowData.deviceType];");
            stringBuilder.AppendLine("    const queryParameter = {");
            stringBuilder.AppendLine("      deviceType: rowData.deviceType,");
            stringBuilder.AppendLine("      id: rowData.id");
            stringBuilder.AppendLine("    };");
            stringBuilder.AppendLine($"    this.httpService.postData(API.{Get_RetrievalDetail_Url}, queryParameter)");
            stringBuilder.AppendLine("      .subscribe((result: any) => {");
            stringBuilder.AppendLine("        if (result && result.code === 200 && result.data) {");
            stringBuilder.AppendLine("          this.detailData = result.data;");
            stringBuilder.AppendLine("          this.displayDetail = true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("      });");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("}");
            var detailTsFilePath = detailFolder + "equip-table.component.ts";
            var detailTsContent = stringBuilder.ToString();
            File.WriteAllText(detailTsFilePath, detailTsContent, new UTF8Encoding(false));
        }
    }
}
