import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EUrl } from '@common/url-api';
import { APIResponse, BaseAPIResponse } from '@models/api-response.model';
import { SizeViewModel } from '@models/lipstick-shop-models/size.model';
import { Pagination } from '@models/pagination.model';
import { AuthenticationService } from '@services/system-services/authentication.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SizeService {

  constructor(private http: HttpClient,private authenticationService: AuthenticationService) { }
  getAll(pageIndex: number, pageSize: number): Observable<APIResponse<Pagination<SizeViewModel>>> {
    return this.http.get<APIResponse<Pagination<SizeViewModel>>>(EUrl.getAllUrlSize + `/${pageIndex}/${pageSize}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<Pagination<SizeViewModel>>>(EUrl.getAllUrlSize + `/${pageIndex}/${pageSize}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getAllActive(): Observable<APIResponse<SizeViewModel[]>> {
    return this.http.get<APIResponse<SizeViewModel[]>>(EUrl.getAllActiveUrlSize, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<SizeViewModel[]>>(EUrl.getAllActiveUrlSize, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getById(id: any): Observable<APIResponse<SizeViewModel>> {
    return this.http.get<APIResponse<SizeViewModel>>(EUrl.getByIdUrlSize + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<SizeViewModel>>(EUrl.getByIdUrlSize + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  create(model: SizeViewModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(EUrl.createUrlSize, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<BaseAPIResponse>(EUrl.createUrlSize, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  update(model: SizeViewModel): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(EUrl.updateUrlSize, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<BaseAPIResponse>(EUrl.updateUrlSize, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  softDelete(id:number):Observable<BaseAPIResponse>{
  return this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlSize+`/${id}`,{headers:this.authenticationService.GetHeaders()}).pipe(
    catchError(error=>{
      if(error.status===401){
        return this.authenticationService.ReNewToken().pipe(
          switchMap(()=>this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlSize+`/${id}`,{headers:this.authenticationService.GetHeaders()}))
        );
      }else{
        return throwError(error);
      }
    })
  );
  }
}
