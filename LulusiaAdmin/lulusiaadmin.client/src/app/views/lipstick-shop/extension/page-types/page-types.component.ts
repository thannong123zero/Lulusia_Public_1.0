import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormCheckInputDirective, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, TableDirective } from '@coreui/angular';
import { PageTypeViewModel } from '@models/lipstick-shop-models/page-type.model';
import { CkeditorComponent } from '@components/ckeditor/ckeditor.component';
import { DataTableComponent } from "@components/data-table/data-table.component";
import { PageInformation, Pagination } from '@models/pagination.model';
import { PageTypeService } from '@services/lipstick-shop-services/page-type.service';
import { ToastService } from '@services/helper-services/toast.service';
import { EColors } from '@common/global';

@Component({
  selector: 'app-page-types',
  templateUrl: './page-types.component.html',
  styleUrl: './page-types.component.scss',
  imports: [TableDirective,  ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule, FormSelectDirective,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, CkeditorComponent, DataTableComponent],
})
export class PageTypesComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  deleteById: number = 0;
  enumList: string[] = [];
  data: Pagination<PageTypeViewModel> = new Pagination<PageTypeViewModel>();
  createForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    label: new FormControl('', Validators.required),
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    name: new FormControl('', Validators.required),
    label: new FormControl('', Validators.required)
  });

  constructor( private pageTypeService : PageTypeService,private toastService : ToastService) {}

  ngOnInit(): void {
    this.getData();
    this.getEPageType();
  }
  getData(){
    this.pageTypeService.getAll(this.pageInformation.pageIndex,this.pageInformation.pageSize).subscribe((res) => {
      this.data = res.data;
      this.pageInformation.currentPage = res.data.currentPage;
      this.pageInformation.totalPages = res.data.totalPages;
      this.pageInformation.totalItems = res.data.totalItems;

    });
  }
  getEPageType() {
    this.pageTypeService.getEPageType().subscribe((res) => {
      this.enumList = res.data;
    });
  }
  onPageIndexChange(index: any) {
    this.pageInformation.pageIndex = index;
    this.getData();
  }
  onPageSizeChange(size: any) {
    this.pageInformation.pageSize = size;
    this.pageInformation.pageIndex = 1;
    this.getData();
  }
//#region  Create Form
  onSubmitCreateForm() {
    console.log(this.createForm.value);
    if (this.createForm.valid) {
      this.pageTypeService.create(this.createForm.value).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.toastService.showToast(EColors.success,res.message);
        this.getData();
        this.getEPageType();
        this.createForm.reset();
        this.createForm.patchValue({priority: 1,pageTypeId: -1, isActive: true});
      });
    }
  }

  toggleLiveCreateModel() {
    this.visibleCreateModal = !this.visibleCreateModal;
  }

  handleLiveCreateModelChange(event: any) {
    this.visibleCreateModal = event;
  }
  get nameCreateForm() { return this.createForm.get('name'); }
  get labelCreateForm() { return this.createForm.get('label'); }
//#endregion
//#region  Update Form
updateData(id: number) {
  this.pageTypeService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res.data);
    this.toggleLiveUpdateModel();
  });
}

onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    this.pageTypeService.update(this.updateForm.value).subscribe((res) => {
      this.toggleLiveUpdateModel();
      this.toastService.showToast(EColors.success,res.message);
      this.getData();
    });
  }
}

toggleLiveUpdateModel() {
  this.visibleUpdateModal = !this.visibleUpdateModal;
}

handleLiveUpdateModelChange(event: any) {
  this.visibleUpdateModal = event;
}

get nameUpdateForm() { return this.updateForm.get('name'); }
get labelUpdateForm() { return this.updateForm.get('label'); }
//#endregion
}