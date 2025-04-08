import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CardBodyComponent, CardComponent, CardHeaderComponent, TableDirective } from '@coreui/angular';
import { AccountService } from '@services/system-services/account.service';
import { AccountModel } from '@models/system-management-models/account.model';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [TableDirective, CardComponent, CardBodyComponent, CardHeaderComponent, NgFor, RouterLink],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})
export class IndexComponent implements OnInit {
  data: AccountModel[] = [];
  constructor(private accountService : AccountService) {}

  ngOnInit() {
    this.accountService.getAccounts().subscribe((res: AccountModel[]) => {
      this.data = res;
      console.log(this.data);
    });
  }

}
