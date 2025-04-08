import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { AccountModel } from '@models/system-management-models/account.model';
import { EUrl } from '@common/url-api';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient, private authenticationService: AuthenticationService) { }

  getAccounts(): Observable<AccountModel[]> {
    return this.http.get<AccountModel[]>(EUrl.getAllUrlAccount, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<AccountModel[]>(EUrl.getAllUrlAccount, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  getAccountById(id: number): Observable<AccountModel> {
    return this.http.get<AccountModel>(`${EUrl.getByIdUrlAccount}${id}`, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.get<AccountModel>(`${EUrl.getByIdUrlAccount}${id}`, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  createAccount(account: AccountModel): Observable<AccountModel> {
    return this.http.post<AccountModel>(EUrl.createUrlAccount, account, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.post<AccountModel>(EUrl.createUrlAccount, account, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  updateAccount(account: AccountModel): Observable<AccountModel> {
    return this.http.put<AccountModel>(EUrl.updateUrlAccount, account, { headers: this.authenticationService.GetHeaders() }).pipe(
      catchError(error => {
        if (error.status === 401) {
          return this.authenticationService.ReNewToken().pipe(
            switchMap(() => this.http.put<AccountModel>(EUrl.updateUrlAccount, account, { headers: this.authenticationService.GetHeaders() }))
          );
        } else {
          return throwError(error);
        }
      })
    );
  }

  // deleteAccount(id: number): Observable<any> {
  //   return this.http.delete<any>(EUrl.deleteUrlAccount + id);
  // }

}
