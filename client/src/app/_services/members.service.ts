import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl : string = environment.apiUrl;
  members : Member[] = [];
  isNewRegister : boolean = false;

  constructor(private http : HttpClient) { }

  getMembers() {
    const user : User = JSON.parse(localStorage.getItem('user')!);

    if(this.members.length > 0 && !this.isNewRegister) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
        this.members = members;
        this.isNewRegister = false;
        return members; 
      }),
      
    )
  }

  getMember(username : string) {
    const member  = this.members.find(x => x.userName === username);
    if(member) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member : Member) {
    return this.http.put(this.baseUrl + 'users',member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member} 
      })
    )
  }

  setMainPhoto(photoId : number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
    
  }

  deletePhoto(photoId : number) {
    return this.http.delete(this.baseUrl +'users/delete-photo/' + photoId);
  }

}
