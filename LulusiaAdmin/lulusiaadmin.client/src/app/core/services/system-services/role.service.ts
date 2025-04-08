import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { RoleModel } from '@models/system-management-models/role.model';
import { EUrl } from '@common/url-api';
import { AuthenticationService } from './authentication.service';
import { APIResponse } from '@models/api-response.model';
import { Pagination } from '@models/pagination.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http : HttpClient,private authenticationService: AuthenticationService) { }

  getAll(pageIndex : number): Observable<APIResponse<Pagination<RoleModel>>> {
    return this.http.get<APIResponse<Pagination<RoleModel>>>(EUrl.getAllUrlRole + `/${pageIndex}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<Pagination<RoleModel>>>(EUrl.getAllUrlRole + `/${pageIndex}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  createRole(role: RoleModel): Observable<RoleModel> {
    return this.http.post<RoleModel>(EUrl.createUrlRole, role, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<RoleModel>(EUrl.createUrlRole, role, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  updateRole(role: RoleModel): Observable<RoleModel> {
    return this.http.put<RoleModel>(EUrl.updateUrlRole, role, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<RoleModel>(EUrl.updateUrlRole, role, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

}
