import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ButtonDirective, CardBodyComponent, CardComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective } from '@coreui/angular';
import { cilCloudUpload } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { CkeditorComponent } from '@components/ckeditor/ckeditor.component';
import { TopicViewModel } from '@models/lipstick-shop-models/topic.model';
import { BlogService } from '@services/lipstick-shop-services/blog.service';
import { TopicService } from '@services/lipstick-shop-services/topic.service';
@Component({
  selector: 'app-update',
  imports: [FormDirective, FormLabelDirective, FormSelectDirective,
    FormControlDirective, ButtonDirective, NgIf, CkeditorComponent,
    RouterLink, CardComponent, CardBodyComponent, IconDirective, NgFor,
    FormCheckComponent, ReactiveFormsModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.scss'
})
export class UpdateComponent {
  topicList: TopicViewModel[] = [];
  reviewUploadImage: string = '';
  icons: any = { cilCloudUpload };
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(-1),
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
  constructor(private topicService: TopicService, private blogService: BlogService, private router : Router, private route: ActivatedRoute, ) { }

  onChangeUploadImage(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      //show image preview
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.reviewUploadImage = `<img src="${e.target.result}" alt="Image Preview" class="mw-100 mh-100"/>`;
        this.updateForm.patchValue({imageFile: file});
      };
      reader.readAsDataURL(file);
    }
  }
  openFileInput() {
    document.getElementById('uploadImage')?.click();
  }
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.blogService.getById(id).subscribe(data => {
      this.updateForm.patchValue(data);
      this.reviewUploadImage = `<img src="https://localhost:7025/blogs/${data.avatar}" alt="Image Preview" class="mw-100 mh-100"/>`;
    });
    this.topicService.getAllActive().subscribe(res => {
      this.topicList = res.data;
    });
  }

  onSubmit(): void {
    debugger;
    if(this.updateForm.valid){
      const formData = new FormData();
      formData.append('id', this.updateForm.value.id);
      formData.append('topicId', this.updateForm.value.topicId);
      formData.append('subjectEN', this.updateForm.value.subjectEN);
      formData.append('subjectVN', this.updateForm.value.subjectVN);
      formData.append('descriptionEN', this.updateForm.value.descriptionEN);
      formData.append('descriptionVN', this.updateForm.value.descriptionVN);
      formData.append('contentEN', this.updateForm.value.contentEN);
      formData.append('contentVN', this.updateForm.value.contentVN);
      formData.append('isActive', this.updateForm.value.isActive);
      formData.append('imageFile', this.updateForm.value.imageFile);
      this.blogService.update(formData).subscribe(res => {
        this.router.navigate(['/lipstick-shop/blogs']);
      });
    }
  }

  composeText(data: string, type: string): void {
    if (type === 'VN')
      this.updateForm.patchValue({ contentVN: data });
    else if (type === 'EN')
      this.updateForm.patchValue({ contentEN: data });
  }


  get topicId() { return this.updateForm.get('topicId'); }
  get subjectEN() { return this.updateForm.get('subjectEN'); }
  get subjectVN() { return this.updateForm.get('subjectVN'); }
  get descriptionEN() { return this.updateForm.get('descriptionEN'); }
  get descriptionVN() { return this.updateForm.get('descriptionVN'); }
  get contentEN() { return this.updateForm.get('contentEN'); }
  get contentVN() { return this.updateForm.get('contentVN'); }
  get isActive() { return this.updateForm.get('isActive'); }


}
