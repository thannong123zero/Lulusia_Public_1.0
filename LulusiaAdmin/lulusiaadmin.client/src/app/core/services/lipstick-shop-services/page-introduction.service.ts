import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EUrl } from '@common/url-api';
import { APIResponse, BaseAPIResponse } from '@models/api-response.model';
import { PageIntroductionViewModel } from '@models/lipstick-shop-models/page-introduction.model';
import { Pagination } from '@models/pagination.model';
import { AuthenticationService } from '@services/system-services/authentication.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PageIntroductionService {

  constructor(private http: HttpClient,private authenticationService: AuthenticationService) { }
  getAll(pageIndex: number, pagePageIntroduction: number): Observable<APIResponse<Pagination<PageIntroductionViewModel>>> {
    return this.http.get<APIResponse<Pagination<PageIntroductionViewModel>>>(EUrl.getAllUrlPageIntroduction + `/${pageIndex}/${pagePageIntroduction}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<Pagination<PageIntroductionViewModel>>>(EUrl.getAllUrlPageIntroduction + `/${pageIndex}/${pagePageIntroduction}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getAllActive(): Observable<APIResponse<PageIntroductionViewModel[]>> {
    return this.http.get<APIResponse<PageIntroductionViewModel[]>>(EUrl.getAllActiveUrlPageIntroduction, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<PageIntroductionViewModel[]>>(EUrl.getAllActiveUrlPageIntroduction, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getById(id: any): Observable<APIResponse<PageIntroductionViewModel>> {
    return this.http.get<APIResponse<PageIntroductionViewModel>>(EUrl.getByIdUrlPageIntroduction + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<PageIntroductionViewModel>>(EUrl.getByIdUrlPageIntroduction + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  create(model: PageIntroductionViewModel): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(EUrl.createUrlPageIntroduction, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<BaseAPIResponse>(EUrl.createUrlPageIntroduction, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  update(model: PageIntroductionViewModel): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(EUrl.updateUrlPageIntroduction, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<BaseAPIResponse>(EUrl.updateUrlPageIntroduction, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  softDelete(id:number):Observable<BaseAPIResponse>{
  return this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlPageIntroduction+`/${id}`,{headers:this.authenticationService.GetHeaders()}).pipe(
    catchError(error=>{
      if(error.status===401){
        return this.authenticationService.ReNewToken().pipe(
          switchMap(()=>this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlPageIntroduction+`/${id}`,{headers:this.authenticationService.GetHeaders()}))
        );
      }else{
        return throwError(error);
      }
    })
  );
  }
}
