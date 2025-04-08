import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EUrl } from '@common/url-api';
import { APIResponse, BaseAPIResponse } from '@models/api-response.model';
import { BrandViewModel } from '@models/lipstick-shop-models/brand.model';
import { Pagination } from '@models/pagination.model';
import { AuthenticationService } from '@services/system-services/authentication.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class BrandService {

  constructor(private http: HttpClient,private authenticationService: AuthenticationService) { }
  getAll(pageIndex: number, pageSize : number): Observable<APIResponse<Pagination<BrandViewModel>>> {
    return this.http.get<APIResponse<Pagination<BrandViewModel>>>(EUrl.getAllUrlBrand + `/${pageIndex}/${pageSize}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<Pagination<BrandViewModel>>>(EUrl.getAllUrlBrand + `/${pageIndex}/${pageSize}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getAllActive(): Observable<APIResponse<BrandViewModel[]>> {
    return this.http.get<APIResponse<BrandViewModel[]>>(EUrl.getAllActiveUrlBrand, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<BrandViewModel[]>>(EUrl.getAllActiveUrlBrand, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getById(id: any): Observable<APIResponse<BrandViewModel>> {
    return this.http.get<APIResponse<BrandViewModel>>(EUrl.getByIdUrlBrand + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<BrandViewModel>>(EUrl.getByIdUrlBrand + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  create(model: FormData): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(EUrl.createUrlBrand, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<BaseAPIResponse>(EUrl.createUrlBrand, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  update(model: FormData): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(EUrl.updateUrlBrand, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<BaseAPIResponse>(EUrl.updateUrlBrand, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  softDelete(id:number):Observable<BaseAPIResponse>{
  return this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlBrand+`/${id}`,{headers:this.authenticationService.GetHeaders()}).pipe(
    catchError(error=>{
      if(error.status===401){
        return this.authenticationService.ReNewToken().pipe(
          switchMap(()=>this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlBrand+`/${id}`,{headers:this.authenticationService.GetHeaders()}))
        );
      }else{
        return throwError(error);
      }
    })
  );
  }
}
