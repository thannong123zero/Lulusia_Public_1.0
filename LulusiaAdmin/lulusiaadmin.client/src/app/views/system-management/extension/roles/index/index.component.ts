import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CardBodyComponent, CardComponent, CardHeaderComponent, TableDirective } from '@coreui/angular';
import { DataTableComponent } from '@components/data-table/data-table.component';
import { APIResponse } from '@models/api-response.model';
import { Pagination } from '@models/pagination.model';
import { RoleModel } from '@models/system-management-models/role.model';
import { RoleService } from '@services/system-services/role.service';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [TableDirective, DataTableComponent , NgFor, RouterLink],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})
export class IndexComponent implements OnInit {
  paging: Pagination<RoleModel> = new Pagination<RoleModel>();
  constructor(private roleService : RoleService) {}
  ngOnInit() {
    this.roleService.getAll(1).subscribe((res : APIResponse<Pagination<RoleModel>>) => {
      debugger;
      if(res.success){
        this.paging = res.data;
      }
    });
  }

}
