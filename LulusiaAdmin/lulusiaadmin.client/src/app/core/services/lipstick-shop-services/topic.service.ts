import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TopicViewModel } from '@models/lipstick-shop-models/topic.model';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { EUrl } from '@common/url-api';
import { AuthenticationService } from '../system-services/authentication.service';
import { Pagination } from '@models/pagination.model';
import { APIResponse, BaseAPIResponse } from '@models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class TopicService {
constructor(private http: HttpClient,private authenticationService: AuthenticationService) { }
  getAll(pageIndex:number , pageSize : number): Observable<APIResponse<Pagination<TopicViewModel>>> {
    return this.http.get<APIResponse<Pagination<TopicViewModel>>>(EUrl.getAllUrlTopic + `/${pageIndex}/${pageSize}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<Pagination<TopicViewModel>>>(EUrl.getAllUrlTopic + `/${pageIndex}/${pageSize}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getAllActive(): Observable<APIResponse<TopicViewModel[]>> {
    return this.http.get<APIResponse<TopicViewModel[]>>(EUrl.getAllActiveUrlTopic, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<TopicViewModel[]>>(EUrl.getAllActiveUrlTopic, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getById(id: any): Observable<APIResponse<TopicViewModel>> {
    return this.http.get<APIResponse<TopicViewModel>>(EUrl.getByIdUrlTopic + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<APIResponse<TopicViewModel>>(EUrl.getByIdUrlTopic + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  create(unit: FormData): Observable<BaseAPIResponse> {
    return this.http.post<BaseAPIResponse>(EUrl.createUrlTopic, unit, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<BaseAPIResponse>(EUrl.createUrlTopic, unit, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  update(unit: FormData): Observable<BaseAPIResponse> {
    return this.http.put<BaseAPIResponse>(EUrl.updateUrlTopic, unit, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<BaseAPIResponse>(EUrl.updateUrlTopic, unit, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  softDelete(id: any): Observable<BaseAPIResponse> {
    return this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlTopic + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.delete<BaseAPIResponse>(EUrl.softDeleteUrlTopic + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
}
