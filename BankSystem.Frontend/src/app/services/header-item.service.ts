import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class HeaderItemService {
  headerItemName = new Subject<string>();
}
