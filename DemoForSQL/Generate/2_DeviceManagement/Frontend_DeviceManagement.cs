using CodeSqlGenerate.Utility;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate.Curd
{
    public static class Frontend_DeviceManagement
    {
        private static readonly string DataManageModulesPath = @"C:\0_Workspace\ict\src\app\ict\content-modules\data-manage\";
        private static readonly Dictionary<string, string> AngularFolderDictionary = new Dictionary<string, string>();

        #region Api_Url
        public static string GetApiUrlName(HotchnerTable table)
        {
            return table.AngularComponentName.Replace('-', '_').ToUpper();
        }
        internal static string Get_Upload_Url(HotchnerTable table)
        {
            var apiUrlName = GetApiUrlName(table);
            return $"UPLOAD_{ apiUrlName}";
        }
        internal static string Get_Upload_Confirm_Url(HotchnerTable table)
        {
            var apiUrlName = GetApiUrlName(table);
            return $"UPLOAD_{ apiUrlName}_CONFIRM";
        }
        internal static string Get_Add_Url(HotchnerTable table)
        {
            var apiUrlName = GetApiUrlName(table);
            return $"ADD_{ apiUrlName}";
        }
        internal static string Get_Update_Url(HotchnerTable table)
        {
            var apiUrlName = GetApiUrlName(table);
            return $"UPDATE_{ apiUrlName}";
        }
        internal static string Get_Delete_Url(HotchnerTable table)
        {
            var apiUrlName = GetApiUrlName(table);
            return $"DELETE_{ apiUrlName}";
        }
        internal static string Get_FIND_ALL_Url(HotchnerTable table)
        {
            var apiUrlName = GetApiUrlName(table);
            return $"FIND_ALL_{ apiUrlName}";
        }
        #endregion

        static Frontend_DeviceManagement()
        {
#warning 这里在新增设备类型时，需要修改
            // 基本设施
            AngularFolderDictionary.Add("Bridge", "basic-equipment\\bridge\\");
            AngularFolderDictionary.Add("Culvert", "basic-equipment\\culvert\\");
            AngularFolderDictionary.Add("LevelCrossing", "basic-equipment\\level-crossing\\");
            AngularFolderDictionary.Add("Marker", "basic-equipment\\marker\\");
            AngularFolderDictionary.Add("PublicCrossingBridge", "basic-equipment\\public-crossing-bridge\\");
            AngularFolderDictionary.Add("Tunnel", "basic-equipment\\tunnel\\");

            // 其他设施
            AngularFolderDictionary.Add("OtherDevice", "other-equipment\\other-device\\");

            // 安防设施
            AngularFolderDictionary.Add("EvacuationRoute", "security\\evacuation-route\\");
            AngularFolderDictionary.Add("PowerComm", "security\\power-comm\\");
            AngularFolderDictionary.Add("ProtectiveFence", "security\\protective-fence\\");
            AngularFolderDictionary.Add("SecurityEquipment", "security\\security-equipment\\");
            AngularFolderDictionary.Add("VideoSurveillance", "security\\video-surveillance\\");
        }

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var folderName = AngularFolderDictionary[table.PascalMethodName];
                var folder = Frontend_Code.AngularCodeOutputPath + folderName;
                CommonMethod.CreateDirectoryIfNotExist(folder);
                if (Program.IsOutputToProject)
                {
                    folder = DataManageModulesPath + folderName;
                }

                var componentName = table.AngularComponentName;
                var tsContent = GenerateTsContent(table);
                var htmlContent = GenerateHtmlContent(table);
                var scssContent = GenerateScssContent(table);

                var tsFilePath = folder + componentName + ".component.ts";
                var htmlFilePath = folder + componentName + ".component.html";
                var scssFilePath = folder + componentName + ".component.scss";

                File.WriteAllText(tsFilePath, tsContent, new UTF8Encoding(false));
                File.WriteAllText(htmlFilePath, htmlContent, new UTF8Encoding(false));
                File.WriteAllText(scssFilePath, scssContent, new UTF8Encoding(false));
            }
        }

        private static string GenerateTsContent(HotchnerTable table)
        {
            var downTemplateName = CommonMethod.GetTemplateName(table);
            var label = table.Label;
            var componentName = table.AngularComponentName;
            var componentClassName = table.PascalMethodName + "Component";
            var formName = GetFormName(table);
            var symbolName = table.PascalMethodName;

            StringBuilder stringBuilder = new StringBuilder();
            #region import
            stringBuilder.AppendLine("import {Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';");
            stringBuilder.AppendLine("import {IctMapComponent} from '../../../../../shared-common/ict-map/ict-map.component';");
            stringBuilder.AppendLine("import {BroadcastType, CommService, ConfirmationService, MessageService} from 'inf-shared';");
            stringBuilder.AppendLine("import {API} from '../../../../../common/api';");
            stringBuilder.AppendLine("import {HttpService} from '../../../../../common/services/http.service';");
            stringBuilder.AppendLine("import {FormControl, FormGroup} from '@angular/forms';");
            stringBuilder.AppendLine("import {MessageItem} from '../../../../../common/models/messageitem';");
            stringBuilder.AppendLine("import {OperatorMode} from '../../../../../common/constants/operator-mode.enum';");
            stringBuilder.AppendLine("import {HTTP_MINIO_BASE_URL} from '../../../../../common/ip-config';");
            stringBuilder.AppendLine("import {SymbolType} from '../../../../../common/constants/symbol-type.enum';");
            stringBuilder.AppendLine("");
            #endregion
            #region Component
            stringBuilder.AppendLine("@Component({");
            stringBuilder.AppendLine("  selector: 'app-" + componentName + "',");
            stringBuilder.AppendLine("  templateUrl: './" + componentName + ".component.html',");
            stringBuilder.AppendLine("  styleUrls: ['./" + componentName + ".component.scss'],");
            stringBuilder.AppendLine("  encapsulation: ViewEncapsulation.None");
            stringBuilder.AppendLine("})");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("export class " + componentClassName + " implements OnInit, OnDestroy {");
            #endregion
            #region private field
            stringBuilder.AppendLine("  dialogHeader: string;");
            stringBuilder.AppendLine("  display: boolean;");
            stringBuilder.AppendLine("  fieldForms: any[];");
            stringBuilder.AppendLine("  sourceData: any[];");
            stringBuilder.AppendLine("  cols: any[];");
            stringBuilder.AppendLine("  @ViewChild(IctMapComponent, {static: false}) ictMap: IctMapComponent;");
            stringBuilder.AppendLine($"  uploadFiles: string = API.{Get_Upload_Url(table)};");
            stringBuilder.AppendLine("  downTemplateUrl: string = HTTP_MINIO_BASE_URL + 'icttemplate/" + downTemplateName + "';");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  private " + formName + ": FormGroup;");
            stringBuilder.AppendLine("  private sub: any;");
            stringBuilder.AppendLine("  private opera: number;");
            stringBuilder.AppendLine("  private symbolType = SymbolType." + symbolName + ";");
            stringBuilder.AppendLine("  private symbolName = '" + symbolName + "';");
            stringBuilder.AppendLine("  private displayPreview: boolean;");
            stringBuilder.AppendLine("  private importDataList: any[];");
            stringBuilder.AppendLine("");
            #endregion
            #region initialize
            stringBuilder.AppendLine("  constructor(");
            stringBuilder.AppendLine("    private messageService: MessageService,");
            stringBuilder.AppendLine("    private httpService: HttpService,");
            stringBuilder.AppendLine("    private commService: CommService,");
            stringBuilder.AppendLine("    private confirmationService: ConfirmationService) {");
            //stringBuilder.AppendLine("    this.sub = this.commService.missionAnnounced$().subscribe((message: MessageItem) => {");
            //stringBuilder.AppendLine("      if (message && message.code === BroadcastType.TREE_NODE) {");
            //stringBuilder.AppendLine("        this.findAllSecurity('100100001');");
            //stringBuilder.AppendLine("      }");
            //stringBuilder.AppendLine("    });");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  ngOnInit() {");
            stringBuilder.AppendLine("    this.findAllSecurity('100100001');");
            stringBuilder.AppendLine("    // 列表中字段名称");
            stringBuilder.AppendLine("    this.cols = [");
            for (int i = 0; i < table.RowList.Count; i++)
            {
                var row = table.RowList[i];
                if (row.Name.ToLower() == "id") continue;
                if (i > Program.DetailViewColumnCount)
                {
                    stringBuilder.AppendLine("//      {field: '" + row.Name.ToLower() + "', header: '" + row.Description + "'},");
                }
                else
                {
                    stringBuilder.AppendLine("      {field: '" + row.Name.ToLower() + "', header: '" + row.Description + "'},");
                }
            }
            stringBuilder.AppendLine("    ];");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("  ngOnDestroy(): void {");
            stringBuilder.AppendLine("    if (this.sub) {");
            stringBuilder.AppendLine("      this.sub.unsubscribe();");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            #endregion
            #region UI Binding
            stringBuilder.AppendLine("  // UI Binding");
            stringBuilder.AppendLine("  addDevice(event) {");
            stringBuilder.AppendLine("    this.opera = OperatorMode.ADD;");
            stringBuilder.AppendLine("    this.dialogHeader = '" + label + "增加';");
            stringBuilder.AppendLine("    this." + formName + " = this.getForm();");
            stringBuilder.AppendLine("    this." + formName + ".get('x').setValue(this.ictMap.mapView.center.longitude);");
            stringBuilder.AppendLine("    this." + formName + ".get('y').setValue(this.ictMap.mapView.center.latitude);");
            stringBuilder.AppendLine("    this.display = true;");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  deleteDevice(rowData) {");
            stringBuilder.AppendLine("    this.confirmationService.confirm({");
            stringBuilder.AppendLine("      message: '确定要删除此设备吗?',");
            stringBuilder.AppendLine("      accept: () => {");
            stringBuilder.AppendLine("        this.httpService.postData(");
            stringBuilder.AppendLine("          API." + Get_Delete_Url(table) + ", {id: rowData.id})");
            stringBuilder.AppendLine("          .subscribe((result: any) => {");
            stringBuilder.AppendLine("            if (result && result.code === 200) {");
            stringBuilder.AppendLine("              this.messageService.add({");
            stringBuilder.AppendLine("                severity: 'success',");
            stringBuilder.AppendLine("                summary: '设备删除',");
            stringBuilder.AppendLine("                detail: '设备删除成功'");
            stringBuilder.AppendLine("              });");
            stringBuilder.AppendLine("              this.findAllSecurity('100100001');");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("          });");
            stringBuilder.AppendLine("      }");
            stringBuilder.AppendLine("    });");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  modifyDevice(rowData) {");
            stringBuilder.AppendLine("    this.dialogHeader = '" + label + "修改';");
            stringBuilder.AppendLine("    this.opera = OperatorMode.MODIFY;");
            stringBuilder.AppendLine("    this." + formName + " = this.getForm();");
            for (int i = 0; i < table.RowList.Count; i++)
            {
                var row = table.RowList[i];
                if (i > Program.DetailViewColumnCount)
                {
                    stringBuilder.AppendLine("//    this." + formName + ".get('" + row.Name.ToLower() + "').setValue(rowData." + row.Name.ToLower() + ");");
                }
                else
                {
                    stringBuilder.AppendLine("    this." + formName + ".get('" + row.Name.ToLower() + "').setValue(rowData." + row.Name.ToLower() + ");");
                }
            }
            stringBuilder.AppendLine("    this.display = true;");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  onDeviceUploaded(event) {");
            stringBuilder.AppendLine("    if (event.originalEvent.body.code === 200) {");
            stringBuilder.AppendLine("      this.importDataList = event.originalEvent.body.data;");
            stringBuilder.AppendLine("      this.displayPreview = true;");
            stringBuilder.AppendLine("    } else {");
            stringBuilder.AppendLine("      this.messageService.add({severity: 'error', summary: '设备导入预览', detail: '设备导入预览失败'});");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("  submitImport() {");
            stringBuilder.AppendLine("    this.httpService.postData(API." + Get_Upload_Confirm_Url(table) + ", this.importDataList)");
            stringBuilder.AppendLine("      .subscribe((result: any) => {");
            stringBuilder.AppendLine("        if (result && result.code === 200) {");
            stringBuilder.AppendLine("          this.findAllSecurity('100100001');");
            stringBuilder.AppendLine("          this.messageService.add({severity: 'success', summary: '设备导入确认', detail: '设备导入确认成功'});");
            stringBuilder.AppendLine("        } else {");
            stringBuilder.AppendLine("          this.messageService.add({severity: 'error', summary: '设备导入确认', detail: '设备导入确认失败'});");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("      });");
            stringBuilder.AppendLine("    this.displayPreview = false;");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  submit" + formName + "(form) {");
            stringBuilder.AppendLine("    if (this.opera === OperatorMode.ADD) {");
            stringBuilder.AppendLine("      this.httpService.postData(API." + Get_Add_Url(table) + ", form)");
            stringBuilder.AppendLine("        .subscribe((result: any) => {");
            stringBuilder.AppendLine("          if (result && result.code === 200) {");
            stringBuilder.AppendLine("            this.display = false;");
            stringBuilder.AppendLine("            this.findAllSecurity('100100001');");
            stringBuilder.AppendLine("            this.messageService.add({severity: 'success', summary: '设备增加', detail: '设备增加成功'});");
            stringBuilder.AppendLine("          } else {");
            stringBuilder.AppendLine("            this.messageService.add({severity: 'error', summary: '设备增加', detail: '设备增加失败'});");
            stringBuilder.AppendLine("          }");
            stringBuilder.AppendLine("        });");
            stringBuilder.AppendLine("    } else if (this.opera === OperatorMode.MODIFY) {");
            stringBuilder.AppendLine("      this.httpService.postData(API." + Get_Update_Url(table) + ", form)");
            stringBuilder.AppendLine("        .subscribe((result: any) => {");
            stringBuilder.AppendLine("          if (result && result.code === 200) {");
            stringBuilder.AppendLine("            this.display = false;");
            stringBuilder.AppendLine("            this.findAllSecurity('100100001');");
            stringBuilder.AppendLine("            this.messageService.add({severity: 'success', summary: '设备修改', detail: '设备修改成功'});");
            stringBuilder.AppendLine("          } else {");
            stringBuilder.AppendLine("            this.messageService.add({severity: 'error', summary: '设备修改', detail: '设备修改失败'});");
            stringBuilder.AppendLine("          }");
            stringBuilder.AppendLine("        });");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  // UI Binding end");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("");
            #endregion
            #region Private method
            stringBuilder.AppendLine("  // Private Method");
            stringBuilder.AppendLine("  findAllSecurity(adminCode) {");
            stringBuilder.AppendLine("    this.httpService.postData(API." + Get_FIND_ALL_Url(table) + ", adminCode)");
            stringBuilder.AppendLine("      .subscribe((result: any) => {");
            stringBuilder.AppendLine("        if (result && result.code === 200 && result.data) {");
            stringBuilder.AppendLine("          this.sourceData = result.data;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("      });");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  getForm(): FormGroup {");
            stringBuilder.AppendLine("    return new FormGroup({");
            for (int i = 0; i < table.RowList.Count; i++)
            {
                var row = table.RowList[i];
                if (i > Program.DetailViewColumnCount)
                {
                    stringBuilder.AppendLine("//      " + row.Name.ToLower() + ": new FormControl(''), // " + row.Description);
                }
                else
                {
                    stringBuilder.AppendLine("      " + row.Name.ToLower() + ": new FormControl(''), // " + row.Description);
                }
            }
            stringBuilder.AppendLine("    });");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  itemSelectEvent(rowData) {");
            stringBuilder.AppendLine("    this.ictMap.gotoPoint(rowData.x, rowData.y);");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  // Private Method End");
            stringBuilder.AppendLine("}");
            #endregion
            return stringBuilder.ToString();
        }
        private static string GenerateHtmlContent(HotchnerTable table)
        {
            var formName = GetFormName(table);
            var label = table.Label;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<div class=\"inf-equipment-up p-grid\">");
            stringBuilder.AppendLine("  <div class=\"inf-equip-right-map p-col\">");
            stringBuilder.AppendLine("    <app-ict-map [displaySymbolType]=\"symbolType\"></app-ict-map>");
            stringBuilder.AppendLine("  </div>");
            stringBuilder.AppendLine("</div>");

            stringBuilder.AppendLine("<inf-module-division [label]=\"'" + label + "列表'\"></inf-module-division>");
            stringBuilder.AppendLine("<div style=\"height: 20px\"></div>");

            #region inf-toolbox
            stringBuilder.AppendLine("<inf-toolbox>");
            stringBuilder.AppendLine("  <ng-template pTemplate=\"infToolContent\">");
            stringBuilder.AppendLine("    <div class=\"tool-container\">");
            stringBuilder.AppendLine("      From");
            stringBuilder.AppendLine("      <inf-calendar class=\"input-element\"></inf-calendar>");
            stringBuilder.AppendLine("      To");
            stringBuilder.AppendLine("      <inf-calendar class=\"input-element\"></inf-calendar>");
            stringBuilder.AppendLine("      <input class=\"inf-input-base input-element\" placeholder=\"请输入查询关键字\">");
            stringBuilder.AppendLine("      <inf-button icon=\"fa fa-search\"></inf-button>");
            stringBuilder.AppendLine("      <div class=\"opera-btn-container\">");
            stringBuilder.AppendLine("        <inf-button [label]=\"'增加'\" (onClick)=\"addDevice($event)\" [icon]=\"'fa fa-plus'\"></inf-button>");
            stringBuilder.AppendLine("        <inf-file-upload multiple=\"single\" name=\"file\"");
            stringBuilder.AppendLine("                         mode=\"basic\"");
            stringBuilder.AppendLine("                         [url]=\"uploadFiles\" maxFileSize=\"100000000\"");
            stringBuilder.AppendLine("                         (onUpload)=\"onDeviceUploaded($event)\"");
            stringBuilder.AppendLine("                         [auto]=\"true\" chooseLabel=\"导入设备\"></inf-file-upload>");
            stringBuilder.AppendLine("        <a style=\"padding-left: 10px\" href=\"{{downTemplateUrl}}\">下载模板</a>");
            stringBuilder.AppendLine("      </div>");
            stringBuilder.AppendLine("    </div>");
            stringBuilder.AppendLine("  </ng-template>");
            stringBuilder.AppendLine("</inf-toolbox>");
            #endregion
            #region data-list-table
            stringBuilder.AppendLine("<div class=\"inf-equip-data-table\">");
            stringBuilder.AppendLine("  <app-data-table [columns]=\"cols\"");
            stringBuilder.AppendLine("                  (editItemChangeEvent)=\"modifyDevice($event)\"");
            stringBuilder.AppendLine("                  (deleteItemEvent)=\"deleteDevice($event)\"");
            stringBuilder.AppendLine("                  (itemSelectEvent)=\"itemSelectEvent($event)\"");
            stringBuilder.AppendLine("                  [value]=\"sourceData\">");
            stringBuilder.AppendLine("  </app-data-table>");
            stringBuilder.AppendLine("</div>");
            #endregion
            #region data-edit-dialog
            stringBuilder.AppendLine("<inf-dialog *ngIf=\"display\"");
            stringBuilder.AppendLine("            header=\"{{dialogHeader}}\"");
            stringBuilder.AppendLine("            [modal]=\"true\"");
            stringBuilder.AppendLine("            [(visible)]=\"display\"");
            stringBuilder.AppendLine("            [style]=\"{width: '750px'}\"");
            stringBuilder.AppendLine("            [minY]=\"70\"");
            stringBuilder.AppendLine("            [maximizable]=\"true\"");
            stringBuilder.AppendLine("            [baseZIndex]=\"-1000\">");
            stringBuilder.AppendLine("  <div class=\"inf-g-12 input-container\">");
            stringBuilder.AppendLine("    <form [formGroup]=\"" + formName + "\"");
            stringBuilder.AppendLine("		  id=\"" + formName + "\"");
            stringBuilder.AppendLine("          (ngSubmit)=\"submit" + formName + "(" + formName + ".value)\">");
            stringBuilder.AppendLine("      <input hidden formControlName=\"id\"/>");
            stringBuilder.AppendLine("      <div class=\"text-input-container\">");

            for (int i = 0; i < table.RowList.Count; i++)
            {
                var row = table.RowList[i];
                if (row.Name.ToUpper() == "ID") { continue; }
                if (i > Program.DetailViewColumnCount) { break; }

                stringBuilder.AppendLine("        <div class=\"inf-g-6 one-input\">");
                stringBuilder.AppendLine("          <div class=\"inf-g-4 input-label\">");
                stringBuilder.AppendLine("            " + row.Description + "：");
                stringBuilder.AppendLine("          </div>");
                stringBuilder.AppendLine("          <div class=\"inf-g-8\">");
                stringBuilder.AppendLine("            <input class=\"inf-input-base\" formControlName=\"" + row.Name.ToLower() + "\">");
                stringBuilder.AppendLine("          </div>");
                stringBuilder.AppendLine("        </div>");
            }

            stringBuilder.AppendLine("      </div>");
            stringBuilder.AppendLine("    </form>");
            stringBuilder.AppendLine("  </div>");
            stringBuilder.AppendLine("  <p-footer>");
            stringBuilder.AppendLine("    <button type=\"submit\" class=\"inf-primary-btn-md\" form=\"" + formName + "\">");
            stringBuilder.AppendLine("      <i class=\"fa fa-check\"></i>");
            stringBuilder.AppendLine("      确定");
            stringBuilder.AppendLine("    </button>");
            stringBuilder.AppendLine("    <button type=\"reset\" class=\"inf-hollow-primary-btn-md\" form=\"" + formName + "\" (click)=\"display=false\">");
            stringBuilder.AppendLine("      <i class=\"fa fa-close\"></i>");
            stringBuilder.AppendLine("      取消");
            stringBuilder.AppendLine("    </button>");
            stringBuilder.AppendLine("  </p-footer>");
            stringBuilder.AppendLine("</inf-dialog>");
            #endregion
            #region Import

            stringBuilder.AppendLine("<inf-dialog *ngIf=\"displayPreview\"");
            stringBuilder.AppendLine("            [modal]=\"true\"");
            stringBuilder.AppendLine("            [style]=\"{width: '750px'}\"");
            stringBuilder.AppendLine("            [(visible)]=\"displayPreview\"");
            stringBuilder.AppendLine("            [minY]=\"70\"");
            stringBuilder.AppendLine("            [maximizable]=\"true\">");
            stringBuilder.AppendLine("  <div style=\"height: 300px; width: 100%\">");
            stringBuilder.AppendLine("    <app-preview-map [importData]=\"importDataList\" [importSymbolType]=\"symbolName\"></app-preview-map>");
            stringBuilder.AppendLine("  </div>");
            stringBuilder.AppendLine("  <p-footer>");
            stringBuilder.AppendLine("    <button type=\"submit\" class=\"inf-primary-btn-md\" (click)=\"submitImport()\">");
            stringBuilder.AppendLine("      <i class=\"fa fa-check\"></i>");
            stringBuilder.AppendLine("      确定");
            stringBuilder.AppendLine("    </button>");
            stringBuilder.AppendLine("    <button type=\"reset\" class=\"inf-hollow-primary-btn-md\" (click)=\"displayPreview=false\">");
            stringBuilder.AppendLine("      <i class=\"fa fa-close\"></i>");
            stringBuilder.AppendLine("      取消");
            stringBuilder.AppendLine("    </button>");
            stringBuilder.AppendLine("  </p-footer>");
            stringBuilder.AppendLine("</inf-dialog>");
            #endregion

            stringBuilder.AppendLine("<inf-confirm-dialog header=\"删除任务\" icon=\"pi pi-exclamation-triangle\"></inf-confirm-dialog>");

            return stringBuilder.ToString();
        }
        private static string GenerateScssContent(HotchnerTable table)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("app-" + table.AngularComponentName + "{");
            stringBuilder.AppendLine("  inf-file-upload {");
            stringBuilder.AppendLine("    .inf-button-text {");
            stringBuilder.AppendLine("      padding: 0.2em 1em 0 1em !important;");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    .inf-button {");
            stringBuilder.AppendLine("      height: 30px;");
            stringBuilder.AppendLine("      margin-top: 5px;");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("  form {");
            stringBuilder.AppendLine("    .input-label {");
            stringBuilder.AppendLine("      margin-top: 10px;");
            stringBuilder.AppendLine("      }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("  }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
        private static string GetFormName(HotchnerTable table)
        {
            return table.PascalMethodName + "Form";
        }
    }
}
