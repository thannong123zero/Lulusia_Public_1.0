import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EUrl } from '@common/url-api';
import { BlogViewModel } from '@models/lipstick-shop-models/blog.model';
import { AuthenticationService } from '@services/system-services/authentication.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private http: HttpClient,private authenticationService: AuthenticationService) { }
  getAll(): Observable<BlogViewModel[]> {
    return this.http.get<BlogViewModel[]>(EUrl.getAllUrlBlog, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<BlogViewModel[]>(EUrl.getAllUrlBlog, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getAllActive(): Observable<BlogViewModel[]> {
    return this.http.get<BlogViewModel[]>(EUrl.getAllActiveUrlBlog, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<BlogViewModel[]>(EUrl.getAllActiveUrlBlog, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getById(id: any): Observable<BlogViewModel> {
    return this.http.get<BlogViewModel>(EUrl.getByIdUrlBlog + `/${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<BlogViewModel>(EUrl.getByIdUrlBlog + `/${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  getByTopicId(topicId: any): Observable<BlogViewModel[]> {
    return this.http.get<BlogViewModel[]>(EUrl.getByTopicIdUrlBlog + `/${topicId}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<BlogViewModel[]>(EUrl.getByTopicIdUrlBlog + `/${topicId}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  create(model: FormData): Observable<BlogViewModel> {
    return this.http.post<BlogViewModel>(EUrl.createUrlBlog, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<BlogViewModel>(EUrl.createUrlBlog, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  update(model: FormData): Observable<BlogViewModel> {
    return this.http.put<BlogViewModel>(EUrl.updateUrlBlog, model, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<BlogViewModel>(EUrl.updateUrlBlog, model, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
  softDelete(id:number):Observable<any>{
  return this.http.delete(EUrl.softDeleteUrlBlog+`/${id}`,{headers:this.authenticationService.GetHeaders()}).pipe(
    catchError(error=>{
      if(error.status===401){
        return this.authenticationService.ReNewToken().pipe(
          switchMap(()=>this.http.delete(EUrl.softDeleteUrlBlog+`/${id}`,{headers:this.authenticationService.GetHeaders()}))
        );
      }else{
        return throwError(error);
      }
    })
  );
  }
}

