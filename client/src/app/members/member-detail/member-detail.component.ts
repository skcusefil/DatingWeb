import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MemberService } from 'src/app/_services/member.service';
import {ActivatedRoute} from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { staticNever } from 'rxjs-compat/add/observable/never';
import { PresenceService } from 'src/app/_services/presence.service';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit, OnDestroy{

  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member:Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  activeTab: TabDirective;
  messages: Message[] = [];
  user: User;

  constructor(public presenceService: PresenceService, 
    private router: ActivatedRoute, 
    private messageService: MessageService,
    private accountService: AccountService) { 

      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    
  }
  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

  ngOnInit(): void {
    
    this.router.data.subscribe(data => {
      this.member = data.member;
    })

    this.router.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
    this.galleryImages = this.getImages();

  }

  getImages(): NgxGalleryImage[]{
    const imageUrl =[];
    for(const photo of this.member.photos){
      imageUrl.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url
      })
    }
    return imageUrl;
  }

  onTabActivated(data: TabDirective){
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) {
      if(this.user)
      {
        this.messageService.createHubConnection(this.user, this.member.username);
      } else {
        this.messageService.stopHubConnection();
      }
      }

  }
  
  loadMessages(){
    this.messageService.getMessageThread(this.member.username).subscribe(messages => {
      this.messages = messages;
    })
  }

  selectTab(tabId:number){
    this.memberTabs.tabs[tabId].active = true;
  }

}

