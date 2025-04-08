import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EColors } from '@common/global';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { cilCloudUpload } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { ActionModel } from '@models/system-management-models/module.model';
import { ToastService } from '@services/helper-services/toast.service';
import { ActionService } from '@services/system-services/action.service';

@Component({
  selector: 'app-actions',
  imports: [TableDirective, CardComponent, ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormDirective, ReactiveFormsModule, FormSelectDirective,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent,
    CardBodyComponent, CardHeaderComponent],
  templateUrl: './actions.component.html',
  styleUrl: './actions.component.scss'
})
export class ActionsComponent {
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  deleteById: number = 0;
  actionList: ActionModel[] = [];
  eActionList: string[] = [];
  reviewCreateUploadImage: string = '';
  reviewUpdateUploadImage: string = '';
  icons : any = {cilCloudUpload};
  createForm: FormGroup = new FormGroup({
    priority: new FormControl(1, [Validators.min(1), Validators.max(100)]),
    name: new FormControl('', Validators.required),
    label: new FormControl('', Validators.required),
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(100)]),
    name: new FormControl('', Validators.required),
    label: new FormControl('', Validators.required),

  });

  constructor(private actionService : ActionService, private toastService: ToastService) {}
  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.actionService.getAll().subscribe((res) => {
      this.actionList = res.data;
    });
    this.actionService.getEAction().subscribe((res) => {
      this.eActionList = res.data;
    });
  }


//#region  Create Form
  onSubmitCreateForm() {
    if (this.createForm.valid) {
      this.actionService.create(this.createForm.value).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.getData();
        this.toastService.showToast(EColors.success,res.message);
        this.createForm.reset();
        this.createForm.get('priority')?.setValue(1);
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
  get priorityCreateForm() { return this.createForm.get('priority'); }
//#endregion
//#region  Update Form
updateData(id: number) {
  this.actionService.getById(id).subscribe((res) => {
    this.updateForm.patchValue({
      id: res.data.id,
      name: res.data.name,
      label: res.data.label,
      priority: res.data.priority,
    });
    this.toggleLiveUpdateModel();
  });
}
onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    this.actionService.update(this.updateForm.value).subscribe((res) => {
      this.toggleLiveUpdateModel();
      this.getData();
      this.toastService.showToast(EColors.success,res.message);
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
get priorityUpdateForm() { return this.updateForm.get('priority'); }
//#endregion

}
