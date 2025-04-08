import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ButtonDirective, CardBodyComponent, CardComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective } from '@coreui/angular';
import { cilCloudUpload } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { CkeditorComponent } from '@components/ckeditor/ckeditor.component';
import { TopicViewModel } from '@models/lipstick-shop-models/topic.model';
import { BlogService } from '@services/lipstick-shop-services/blog.service';
import { TopicService } from '@services/lipstick-shop-services/topic.service';

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
  topicList: TopicViewModel[] = [];
  reviewUploadImage: string = '';
  icons: any = { cilCloudUpload };
  createForm: FormGroup = new FormGroup({
    topicId: new FormControl(-1, Validators.min(1)),
    subjectEN: new FormControl('', Validators.required),
    subjectVN: new FormControl('', Validators.required),
    descriptionEN: new FormControl('', Validators.required),
    descriptionVN: new FormControl('', Validators.required),
    contentEN: new FormControl('', Validators.required),
    contentVN: new FormControl('', Validators.required),
    isActive: new FormControl(true, Validators.required),
    imageFile: new FormControl(null,Validators.required)
  });
  constructor(private topicService: TopicService, private blogService: BlogService, private router : Router) { }

  onChangeUploadImage(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      //show image preview
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.reviewUploadImage = `<img src="${e.target.result}" alt="Image Preview" class="mw-100 mh-100"/>`;
        this.createForm.patchValue({imageFile: file});
      };
      reader.readAsDataURL(file);
    }
  }
  openFileInput() {
    document.getElementById('uploadImage')?.click();
  }
  ngOnInit(): void {
    this.topicService.getAllActive().subscribe(res => {
      this.topicList = res.data;
    });
  }

  onSubmit(): void {
    debugger;
    if(this.createForm.valid){
      const formData = new FormData();
      formData.append('topicId', this.createForm.value.topicId);
      formData.append('subjectEN', this.createForm.value.subjectEN);
      formData.append('subjectVN', this.createForm.value.subjectVN);
      formData.append('descriptionEN', this.createForm.value.descriptionEN);
      formData.append('descriptionVN', this.createForm.value.descriptionVN);
      formData.append('contentEN', this.createForm.value.contentEN);
      formData.append('contentVN', this.createForm.value.contentVN);
      formData.append('isActive', this.createForm.value.isActive);
      formData.append('imageFile', this.createForm.value.imageFile);
      this.blogService.create(formData).subscribe(res => {
        this.router.navigate(['/lipstick-shop/blogs']);
      });
    }
  }

  composeText(data: string, type: string): void {
    if (type === 'VN')
      this.createForm.patchValue({ contentVN: data });
    else if (type === 'EN')
      this.createForm.patchValue({ contentEN: data });
  }


  get topicId() { return this.createForm.get('topicId'); }
  get subjectEN() { return this.createForm.get('subjectEN'); }
  get subjectVN() { return this.createForm.get('subjectVN'); }
  get descriptionEN() { return this.createForm.get('descriptionEN'); }
  get descriptionVN() { return this.createForm.get('descriptionVN'); }
  get contentEN() { return this.createForm.get('contentEN'); }
  get contentVN() { return this.createForm.get('contentVN'); }
  get isActive() { return this.createForm.get('isActive'); }


}
