import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ButtonDirective, CardBodyComponent, CardComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective } from '@coreui/angular';
import { cilCloudUpload } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { CkeditorComponent } from '@components/ckeditor/ckeditor.component';
import { CategoryViewModel } from '@models/lipstick-shop-models/category.model';
import { SubCategoryViewModel } from '@models/lipstick-shop-models/sub-category.model';
import { BlogService } from '@services/lipstick-shop-services/blog.service';
import { CategoryService } from '@services/lipstick-shop-services/category.service';
import { SubCategoryService } from '@services/lipstick-shop-services/sub-category.service';
@Component({
  selector: 'app-create',
  imports: [FormDirective, FormLabelDirective, FormSelectDirective,
    FormControlDirective, ButtonDirective, NgIf, CkeditorComponent,
    RouterLink, CardComponent, CardBodyComponent, IconDirective, NgFor,
    FormCheckComponent, ReactiveFormsModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent {
  saleOff: boolean = false;
  categories: CategoryViewModel[] = [];
  subCategories: SubCategoryViewModel[] = [];
  reviewUploadAvatar: string = '';
  reviewUploadBackGroundImage: string = '';
  reviewUploadImages: string = '';
  icons: any = { cilCloudUpload };
  createForm: FormGroup = new FormGroup({
    id: new FormControl(-1, Validators.min(1)),
    categoryId: new FormControl(-1, Validators.required),
    subCategoryId: new FormControl('', Validators.required),
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    descriptionEN: new FormControl('', Validators.required),
    descriptionVN: new FormControl('', Validators.required),
    detailsEN: new FormControl('', Validators.required),
    detailsVN: new FormControl('', Validators.required),
    isActive: new FormControl(true, Validators.required),
    quantity: new FormControl(0, Validators.required),
    discountPercent: new FormControl(0),
    salePrice: new FormControl(0),
    startDiscountDate: new FormControl(new Date()),
    endDiscountDate: new FormControl(new Date()),
    avatarFile: new FormControl(null,Validators.required),
    backgroundFile: new FormControl(null,Validators.required),
    imageFiles: new FormControl(null,Validators.required)
  });
  constructor(private subCategoryService: SubCategoryService, private categoryService: CategoryService, private router : Router) { }

  onChangeSaleOff(event: any): void {
    this.saleOff = event.target.checked;
  }

  onChangeUploadImageFile(event: any,type: string): void {
    const file: File = event.target.files[0];
    if (file) {
      //show image preview
      const reader = new FileReader();
      reader.onload = (e: any) => {
        if(type === 'avatar')
        {
          this.reviewUploadAvatar = `<img src="${e.target.result}" alt="Image Preview" class="mw-100 mh-100"/>`;
          this.createForm.patchValue({ avatarFile: file });
        }
        else if(type === 'background')
        {
          this.reviewUploadBackGroundImage = `<img src="${e.target.result}" alt="Image Preview" class="mw-100 mh-100"/>`;
          this.createForm.patchValue({ backgroundFile: file });
        }
      };
      reader.readAsDataURL(file);
    }
  }
  onChangeUploadImageFiles(event: any): void {
    const files: FileList = event.target.files;
    if (files && files.length > 0) {
      this.reviewUploadImages = '';
      const fileArray: File[] = Array.from(files);
      fileArray.forEach((file, index) => {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.reviewUploadImages += `<img src="${e.target.result}" alt="Image Preview ${index + 1}" class="mw-100 mh-100"/>`;
          if (index === fileArray.length - 1) {
            this.createForm.patchValue({ imageFiles: fileArray });
          }
        };
        reader.readAsDataURL(file);
      });
    }
  }

  openFileInput(id: string): void {
    document.getElementById(id)?.click();
  }
  ngOnInit(): void {
    this.categoryService.getAllActive().subscribe(res => {
      this.categories = res.data;
    });
  }
  onChangeCategory(event: any) {
    const categoryId = event.target.value;
    if(categoryId === '-1'){
      this.subCategories = [];
      return;
    };
    this.subCategoryService.getByCategoryId(categoryId).subscribe(res => {
      this.subCategories = res.data;
    });
  }

  onSubmit(): void {
    debugger;
    if(this.createForm.valid){
      const formData = new FormData();
      // formData.append('topicId', this.createForm.value.topicId);
      // formData.append('subjectEN', this.createForm.value.subjectEN);
      // formData.append('subjectVN', this.createForm.value.subjectVN);
      // formData.append('descriptionEN', this.createForm.value.descriptionEN);
      // formData.append('descriptionVN', this.createForm.value.descriptionVN);
      // formData.append('contentEN', this.createForm.value.contentEN);
      // formData.append('contentVN', this.createForm.value.contentVN);
      // formData.append('isActive', this.createForm.value.isActive);
      // formData.append('imageFile', this.createForm.value.imageFile);
      // this.blogService.create(formData).subscribe(res => {
      //   this.router.navigate(['/lipstick-shop/blogs']);
      // });
    }
  }

  composeText(data: string, type: string): void {
    if (type === 'VN')
      this.createForm.patchValue({ contentVN: data });
    else if (type === 'EN')
      this.createForm.patchValue({ contentEN: data });
  }


get categoryId() { return this.createForm.get('categoryId'); }
get subCategoryId() { return this.createForm.get('subCategoryId'); }
get nameEN() { return this.createForm.get('nameEN'); }
get nameVN() { return this.createForm.get('nameVN'); }
get descriptionEN() { return this.createForm.get('descriptionEN'); }
get descriptionVN() { return this.createForm.get('descriptionVN'); }
get detailsEN() { return this.createForm.get('detailsEN'); }
get detailsVN() { return this.createForm.get('detailsVN'); }



}
