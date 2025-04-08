import { NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ButtonDirective, CardBodyComponent, CardComponent, CollapseDirective, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective } from '@coreui/angular';
import {RoleModel,ModuleModel} from '@models/system-management-models/role.model';
import { DashboardComponent } from "../../../../dashboard/dashboard.component";
import { identifierName } from '@angular/compiler';
import {cilArrowThickBottom,cilArrowThickRight} from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { RoleService } from '@services/system-services/role.service';

@Component({
  selector: 'app-create',
  standalone: true,
  imports: [FormDirective, FormLabelDirective,
    FormControlDirective, ButtonDirective, CollapseDirective, IconDirective,
    RouterLink, CardComponent, CardBodyComponent, NgFor, NgIf,
    FormCheckComponent, ReactiveFormsModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent implements OnInit {
  //public slideTheme: RoleModel = new RoleModel();
  icons = {cilArrowThickBottom,cilArrowThickRight};
  modules: ModuleModel[] = [];
  visible: Array<boolean> = [];
  createForm : FormGroup = new FormGroup({
    name : new FormControl('',[Validators.required]),
    description : new FormControl(''),
    isActive: new FormControl(true),
    //selectedRoleClaims: new FormGroup(),
  });
  constructor(private roleService : RoleService,private router: Router) {}
  ngOnInit(): void {
    //this.getModules();
  }

  toggleCollapse(id:number){
    this.visible[id] = !this.visible[id];
  }
  onSubmit() {
    this.roleService.createRole(this.createForm.value).subscribe(
      (result) => {
        // How to redirect to page index ?
        this.router.navigate(['/roles'])
      },
      (error) => {
        console.error(error);
      }
    );
  }
  // createSlideTheme() {
  //   this.http.post<RoleModel>('/api/slidetheme/create', this.formData.value).subscribe(
  //     (result) => {
  //       // How to redirect to page index ?
  //       this.router.navigate(['/slide-theme'])
  //     },
  //     (error) => {
  //       console.error(error);
  //     }
  //   );
  // }
}
