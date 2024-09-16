import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class PermissionsService {
  constructor() {}

  canAccess(permissionItem: number, permissionUser: number): boolean {
    if (permissionUser === -1) {
      return true;
    }

    if ((permissionItem & permissionUser) === permissionItem) {
      return true;
    }

    return false;
  }
}
