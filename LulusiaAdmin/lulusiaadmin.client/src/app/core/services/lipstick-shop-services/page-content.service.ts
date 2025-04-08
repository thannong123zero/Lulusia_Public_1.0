import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EUrl } from '@common/url-api';
import { APIResponse, BaseAPIResponse } from '@models/api-response.model';
import { PageContentViewModel } from '@models/lipstick-shop-models/page-content.model';
import { Pagination } from '@models/pagination.model';
import { AuthenticationService } from '@services/system-services/authentication.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PageContentService {

  constructor(private http: HttpClient,private authenticationService: AuthenticationService) { }
  getAll(pageIndex: number, pagePageContent: number, pageTypeId : number): Observable<APIResponse<Pagination<PageContentViewModel>>> {
    return this.http.get<APIResponse<Pagination<PageContentViewModel>>>(EUrl.getAllUrlPageContent + `/${pageIndex}/${pagePageContent}/${pageTypeId}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<Pagination<PageContentViewModel>>>(EUrl.getAllUrlPageContent + `/${pageIndex}/${pagePageContent}/${pageTypeId}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getAllActive(): Observable<APIResponse<PageContentViewModel[]>> {
    return this.http.get<APIResponse<PageContentViewModel[]>>(EUrl.getAllActiveUrlPageContent, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<PageContentViewModel[]>>(EUrl.getAllActiveUrlPageContent, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getById(id: any): Observable<APIResponse<PageContentViewModel>> {
    return this.http.get<APIResponse<PageContentViewModel>>(EUrl.getByIdUrlPageContent + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<PageContentViewModel>>(EUrl.getByIdUrlPageContent + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  create(model: PageContentViewModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(EUrl.createUrlPageContent, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<BaseAPIResponse>(EUrl.createUrlPageContent, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  update(model: PageContentViewModel): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(EUrl.updateUrlPageContent, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<BaseAPIResponse>(EUrl.updateUrlPageContent, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  softDelete(id:number):Observable<BaseAPIResponse>{
  return this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlPageContent+`/${id}`,{headers:this.authenticationService.GetHeaders()}).pipe(
    catchError(error=>{
      if(error.status===401){
        return this.authenticationService.ReNewToken().pipe(
          switchMap(()=>this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlPageContent+`/${id}`,{headers:this.authenticationService.GetHeaders()}))
        );
      }else{
        return throwError(error);
      }
    })
  );
  }
}
