import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { ProductViewModel } from '@models/lipstick-shop-models/product.model';
import { ProductService } from '@services/lipstick-shop-services/product.service';
@Component({
  selector: 'app-index',
  imports: [TableDirective, CardComponent, ModalBodyComponent, NgFor, RouterLink,
    ModalComponent, ButtonDirective, ReactiveFormsModule, FormSelectDirective,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent,
    CardBodyComponent, CardHeaderComponent],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})
export class IndexComponent {

}
