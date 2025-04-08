import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AccordionButtonDirective, AccordionComponent, AccordionItemComponent, ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective, TemplateIdDirective } from '@coreui/angular';
import { ProductViewModel } from '@models/lipstick-shop-models/product.model';
import { ProductService } from '@services/lipstick-shop-services/product.service';

@Component({
  selector: 'app-index',
  imports: [TableDirective, CardComponent, ModalBodyComponent, NgFor, RouterLink,
    ModalComponent, ButtonDirective, ReactiveFormsModule,  AccordionButtonDirective,
    AccordionComponent,
    AccordionItemComponent,
    TemplateIdDirective,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent,
    CardBodyComponent, CardHeaderComponent],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})
export class IndexComponent {
  visibleDelete: boolean = false;
  deleteById: number = -1;
  topicId: number = -1;
  productList: ProductViewModel[] = [];

  constructor(private productService : ProductService) {}
  ngOnInit(): void {
    this.getData();
  }
  getData(){
    this.productService.getAll().subscribe((res) => {
      this.productList = res;
    });
  }


  //#region Delete
  deleteData(id: number) {
    this.deleteById = id;
    this.toggleLiveDelete();
  }
  deleteDataConfirm() {
    this.productService.softDelete(this.deleteById).subscribe(() => {
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
