import {
  AfterContentInit,
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  HostBinding,
  Input
} from '@angular/core';

import { IconDirective } from '@coreui/icons-angular';
import { RouterLink } from '@angular/router';
import { NavComponent, NavItemComponent, NavLinkDirective } from '@coreui/angular';

@Component({
    selector: 'app-docs-example',
    templateUrl: './docs-example.component.html',
    styleUrls: ['./docs-example.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [NavComponent, NavItemComponent, NavLinkDirective, RouterLink, IconDirective]
})
export class DocsExampleComponent implements AfterContentInit, AfterViewInit {

  constructor(
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  @Input() fragment?: string;

  @HostBinding('class.example')
  get exampleClass() {
    return true;
  };

  private _href = 'https://coreui.io/angular/docs/';

  get href(): string {
    return this._href;
  }

  @Input()
  set href(value: string) {
    const version = 5.4;
    const docsUrl = 'https://coreui.io/angular/';
    // const path: string = version ? `${version}/${value}` : '';
    this._href = `${docsUrl}${value}`;
  }

  ngAfterContentInit(): void {
    // this.changeDetectorRef.detectChanges();
  }

  ngAfterViewInit(): void {
    this.changeDetectorRef.markForCheck();
  }
}
