import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { baseUrl } from '@common/global';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { BlogViewModel } from '@models/lipstick-shop-models/blog.model';
import { TopicViewModel } from '@models/lipstick-shop-models/topic.model';
import { BlogService } from '@services/lipstick-shop-services/blog.service';
import { TopicService } from '@services/lipstick-shop-services/topic.service';

@Component({
  selector: 'app-index',
  imports: [TableDirective, CardComponent, ModalBodyComponent, NgFor,RouterLink,
    ModalComponent, ButtonDirective, ReactiveFormsModule,FormSelectDirective,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent,NgIf,
    CardBodyComponent, CardHeaderComponent],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})

export class IndexComponent implements OnInit {
  visibleDelete: boolean = false;
  deleteById: number = -1;
  topicId: number = -1;
  blogList: BlogViewModel[] = [];
  topicList: TopicViewModel[] = [];
  baseUrl:string = baseUrl;


  constructor(private topicService : TopicService, private blogService : BlogService) {}

  ngOnInit(): void {
    this.getData();
    this.topicService.getAllActive().subscribe((res) => {
      this.topicList = res.data;
    });
  }
  getData(){
    this.blogService.getByTopicId(this.topicId).subscribe((res) => {
      this.blogList = res;
    });
  }
  filter(topic: any) {
    this.topicId = topic.target.value;
    this.blogService.getByTopicId(this.topicId).subscribe((res) => {
      this.blogList = res;
    });
  }

//#region Delete
deleteData(id: number) {
  this.deleteById = id;
  this.toggleLiveDelete();
}
deleteDataConfirm() {
  this.blogService.softDelete(this.deleteById).subscribe(() => {
    this.toggleLiveDelete();
    this.getData();
  });
}
toggleLiveDelete() {
  this.visibleDelete = !this.visibleDelete;
}

handleLiveDeleteChange(event: any) {
  this.visibleDelete = event;
}
//#endregion
}
