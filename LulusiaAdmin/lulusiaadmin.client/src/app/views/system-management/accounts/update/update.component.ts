import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ButtonDirective, CardBodyComponent, CardComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective } from '@coreui/angular';
import { AccountErrorModel, AccountModel } from '@models/system-management-models/account.model';
import { RoleModel } from '@models/system-management-models/role.model';
import { AccountService } from '@services/system-services/account.service';
import { RoleService } from '@services/system-services/role.service';
import { userNameValidator } from '@common/validations/username.validator';
import { phoneNumberValidator } from '@common/validations/phonenumber.validator';
import { fullNameValidator } from '@common/validations/fullname.validator';
@Component({
  selector: 'app-update',
  standalone: true,
  imports: [ FormDirective, FormLabelDirective, NgFor, NgIf,
    FormControlDirective, ButtonDirective,FormSelectDirective,
     RouterLink, CardComponent, CardBodyComponent,
      FormCheckComponent, ReactiveFormsModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.scss'
})
export class UpdateComponent implements OnInit {  
  roles : RoleModel[] = [];
  errorModel: AccountErrorModel = {
    //fullName: [],
    userName: [],
    email: [],
    phoneNumber: [],
    password: [],
    roleId: [],
  };
  formData : FormGroup = new FormGroup({
    id : new FormControl(-1),
    mallId : new FormControl(-1),
    officeId : new FormControl(-1),
    roleId : new FormControl(-1,Validators.min(1)),
    //fullName : new FormControl('',[Validators.required, fullNameValidator()]),
    userName : new FormControl('',[Validators.required, userNameValidator()]),
    email : new FormControl('',[Validators.required, Validators.email]),
    phoneNumber : new FormControl('',phoneNumberValidator()),
    password : new FormControl(''),
    isActive: new FormControl(true),
  });
  
  constructor(private accountService : AccountService,
     private roleService : RoleService,
     private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id:any = this.route.snapshot.paramMap.get('id');
    // this.roleService.getRoles().subscribe((res: RoleModel[]) => {
    //   this.roles = res;
    // });
    this.accountService.getAccountById(id).subscribe((res: AccountModel) => {
      this.formData.patchValue(res);
      this.formData.get('password')?.setValue('Sv@123456');
    });
  }

  onSubmit() {
    console.log(this.formData.value);
    this.accountService.updateAccount(this.formData.value).subscribe(() => {
      this.router.navigate(['/accounts']);
    }, (errObj : any) => {
      this.errorModel.email = errObj?.error?.errors?.Email;
      //this.errorModel.fullName = errObj?.error?.errors?.FullName;
      this.errorModel.userName = errObj?.error?.errors?.UserName;
      this.errorModel.phoneNumber = errObj?.error?.errors?.PhoneNumber;
      this.errorModel.password = errObj?.error?.errors?.Password;
      this.errorModel.roleId = errObj?.error?.errors?.RoleId;
    });
  }
  
  get fullName() {
    return this.formData.get('fullName');
  }
  get email() {
    return this.formData.get('email');
  }
  get phoneNumber() {
    return this.formData.get('phoneNumber');
  }
  get roleId() {
    return this.formData.get('roleId');
  }

}
